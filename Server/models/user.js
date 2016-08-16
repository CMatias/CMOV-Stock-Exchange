var Mongoose = require('../database').Mongoose;

//create the schema
var userSchema = new Mongoose.Schema({
    uri: {
        type: String
    },
    stocks: [{
        code: {
            type: String
        },
        lowerLimit: {
            type: Number
        },
        upperLimit: {
            type: Number
        }
    }],
    portfolioLowerLimit: {
        type: Number
    },
    portfolioUpperLimit: {
        type: Number
    },
    creationDate: {
        type: Date,
        default: Date.now
    }
});

module.exports = Mongoose.model('User', userSchema, 'Users');