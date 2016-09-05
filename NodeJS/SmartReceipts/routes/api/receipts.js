var express = require('express');
var router = express.Router();

/* GET home page. */
router.route('/send').post(function(req, res, next) {
    var name = req.body.Name
    var output = {"response": name};
    res.end( JSON.stringify(output));
});

module.exports = router;
