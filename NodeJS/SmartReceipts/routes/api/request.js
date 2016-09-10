var express = require('express');
var router = express.Router();

/* GET home page. */
router.route('/priview').post(function(req, res, next) {

    var startDate = req.body.StartDate;
    var startDate = req.body.EndDate;
    var cluntID = req.body.ID;

    if (req.body.filterType == "name") {
        // select by name
    }
    else if (re.body.filterType == "category") {
        // select by category
    }
    else {
        //select only bydates
    }
});

module.exports = router;
