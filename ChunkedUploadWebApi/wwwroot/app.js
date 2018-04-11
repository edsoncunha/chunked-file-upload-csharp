var express = require('express');
//var resumable = require('./resumable-node.js')('/tmp/resumable.js/');
var app = express();


app.use(express.static(__dirname + '/public'));

app.get('/resumable.js', function (req, res) {
  var fs = require('fs');
  res.setHeader("content-type", "application/javascript");
  fs.createReadStream("resumable.js").pipe(res);
});

app.listen(3000, function () {
  console.log('client running on http://localhost:3000');
});

