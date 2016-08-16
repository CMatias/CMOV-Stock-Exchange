var Joi = require('joi');
var User = require('../models/user');

exports.getUsers = {
    handler: function(request, reply) {
        User.find(function (err, users) {
            if (err) {
                return reply(err);
            }
            reply(users);
        });
    }
};

exports.getUser = {
    validate: {
        params: {
            uri: Joi.string().required()
        }
    },
    handler: function(request, reply) {
        User.findOne({ 'uri': request.params.uri }, function(err, user) {
            if(user === null){
                return reply('Not Found');
            }

            if (err) {
               return reply(err);
            }

            return reply(user);
        });
    }
};

exports.postUser = {
    validate: {
        payload: {
            uri: Joi.string().required(),
            stocks: Joi.array().required(),
            portfolioLowerLimit: Joi.number().positive(),
            portfolioUpperLimit: Joi.number().positive()
        }
    },
    handler: function(request, reply) {

        var user = new User();

        user.uri = request.payload.uri;
        user.stocks = request.payload.stocks;
        user.portfolioLowerLimit = request.payload.portfolioLowerLimit;
        user.portfolioUpperLimit = request.payload.portfolioUpperLimit;

        user.save(function(err) {
            if (err) {
                return reply(err);
            }

            reply('Created');
        });
    }
};

exports.putUser = {
    validate: {
        payload: {
            uri: Joi.string().required(),
            newuri: Joi.string(),
            stocks: Joi.array(),
            portfolioLowerLimit: Joi.number().positive(),
            portfolioUpperLimit: Joi.number().positive()
        }
    },
    handler: function(request, reply) {

        User.findOne({ 'uri': request.payload.uri }, function(err, user) {

            if(user === null){
                return reply('Not Found');
            }

            if (err) {
                return reply(err);
            }

            if(request.payload.newuri){
                user.uri = request.payload.newuri;
            }

            if(request.payload.stocks){
                user.stocks = request.payload.stocks;
            }

            if(request.payload.portfolioLowerLimit){
                user.portfolioLowerLimit = request.payload.portfolioLowerLimit;
            }

            if(request.payload.portfolioUpperLimit) {
                user.portfolioUpperLimit = request.payload.portfolioUpperLimit;
            }

            user.save(function(err) {
                if (err) {
                    return reply(err);
                }
                return reply('Updated');
            });
        });
    }
};
