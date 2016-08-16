var Users = require('../controllers/users');

exports.endpoints = [
    { method: 'GET', path: '/users', config: Users.getUsers },
    { method: 'GET', path: '/user/{uri}', config: Users.getUser },
    { method: 'POST', path: '/user', config: Users.postUser },
    { method: 'PUT', path: '/user', config: Users.putUser }
];