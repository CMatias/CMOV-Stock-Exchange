var wns = require('wns');

//Client id and client secret are windows app dependent.

module.exports = {
  sendToastNotification: function(uri, message1, message2){

      var channelUrl = uri;
      var options = {
          client_id: 'ms-app://s-1-15-2-409372313-1779182144-1813304920-1661312474-4243298450-1235029540-670769999',
          client_secret: 'ea3tAu0FN12XcVf6V0guFSUo1INKgIwJ'
      };

      wns.sendToastText02(channelUrl, message1, message2, options, function (error, result) {
          if (error) {
              console.log(error);
          }
          else {
              console.log(result);
          }
      });
  },
  sendTileNotification: function(uri, message1, message2){

      var channelUrl = uri;
      var options = {
          client_id: 'ms-app://s-1-15-2-409372313-1779182144-1813304920-1661312474-4243298450-1235029540-670769999',
          client_secret: 'ea3tAu0FN12XcVf6V0guFSUo1INKgIwJ'
      };

      wns.sendTileSquareText02(channelUrl, message1, "" + message2, options, function (error, result) {
          if (error) {
              console.log(error);
          }
          else {
              console.log(result);
          }
      });
  }
};
