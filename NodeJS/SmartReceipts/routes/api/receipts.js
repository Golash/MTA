var express = require('express');
var router = express.Router();
var RECEIPTS_COLLECTION = "receipts";

// Generic error handler used by all endpoints.
function handleError(res, reason, message, code) {
    console.log("ERROR: " + reason);
    res.status(code || 500).json({"error": message});
}

router.route('/send').post(function(req, res, next) {
    var db = req.db;
    var newReceipt = req.body;
    newReceipt.createDate = new Date();


    db.collection(RECEIPTS_COLLECTION).insertOne(newReceipt, function(err, doc) {
        if (err) {
            handleError(res, err.message, "Failed to create new receipts.");
        } else {
            res.status(201).json(doc.ops[0]);
        }
    });
});

module.exports = router;
