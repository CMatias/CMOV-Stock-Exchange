var Hapi = require('hapi');
var Routes = require('./routes');
var yahooFinance = require('yahoo-finance');
var _ = require('underscore');
var User = require('./models/user');
var windowsNotifications = require('./services/windows_notifications.js');

var server = new Hapi.Server();
server.connection({
    port: 1337
});

// Print some information about the incoming request for debugging purposes
server.ext('onRequest', function (request, reply) {
    console.log(request.path, request.query);
    return reply.continue();
});

server.route(Routes.endpoints);

server.start(function () {
    console.log('Server running at:', server.info.uri);
});

var pollingLoop = function() {

    User.find(function (err, users) {
        if (err) {
            console.log(err);
        }

        _.each(users, function(user){
            var stocks = _.map(user.stocks, function(stock){
                if (stock.code)
                    return stock.code;
                return null;
            });

            var uri = user.uri;

            yahooFinance.snapshot({
                symbols: stocks,
                fields: ['s', 'n', 'd1', 'l1', 'y', 'r']
            }, function (err, snapshot) {
                var userPortfolio = 0;
                var mainStock = _.max(snapshot, function(stock){ return stock.lastTradePriceOnly; });

                windowsNotifications.sendTileNotification(uri, mainStock.symbol, mainStock.lastTradePriceOnly);

                var stocksAbove = "";
                var stocksBelow = "";

                _.each(user.stocks, function(stock){

                    snapshot.forEach( function(snapshotElement){
                        if(snapshotElement.symbol == stock.code){
                            var stockPrice = snapshotElement.lastTradePriceOnly;
                            userPortfolio += stockPrice;

                            if(stock.upperLimit > 0 && stockPrice > stock.upperLimit){
                                stocksAbove = stocksAbove + " " + stock.code;
                            } else if (stock.lowerLimit > 0 && stockPrice < stock.lowerLimit){
                                stocksBelow = stocksBelow + " " + stock.code;
                            }
                        }
                    });

                });

                if(stocksAbove !== ""){
                    windowsNotifications.sendToastNotification(uri, 'Stocks Above Limit', stocksAbove);
                }

                if(stocksBelow !== ""){
                    windowsNotifications.sendToastNotification(uri, 'Stocks Below Limit', stocksBelow);
                }

                if(user.portfolioUpperLimit > 0 && userPortfolio > user.portfolioUpperLimit){
                    windowsNotifications.sendToastNotification(uri, 'StackExchange', 'Your Portfolio is above the limit!');
                } else if (user.portfolioUpperLimit > 0 && userPortfolio < user.portfolioLowerLimit){
                    windowsNotifications.sendToastNotification(uri, 'StackExchange', 'Your Portfolio is below the limit!');
                }

            });
        });
    });

    setTimeout(pollingLoop, 1000 * 60);
};
pollingLoop();
