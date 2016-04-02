var express = require('express');
var bodyParser = require('body-parser');
var app = express();

app.use(bodyParser.urlencoded({ extended: false }))

// parse application/json
app.use(bodyParser.json());

app.use(function (req, res) {
  res.setHeader('Content-Type', 'text/plain')
  res.write('you posted:\n')
  res.end(JSON.stringify(req.body, null, 2))
});

// POST /login gets urlencoded bodies
app.post('/saveEvent', urlencodedParser, function (req, res) {
  if (!req.body) return res.sendStatus(400)
  res.send('welcome, ' + req.body.username)
})	

app.get('/', function (req, res) {
  res.send('Hello World!');
});

app.listen(3000, function () {
  console.log('Example app listening on port 3000!')	;
});

// var MAILJET_KEY = "5076e26d6d0d5a10c84e8110b6336219";
// var MAILJET_PRI = "226a5f03fd539016facc0fd21a5a5972";

// var mailjet = require ('node-mailjet')
//     .connect(MAILJET_KEY, MAILJET_PRI)
// var request = mailjet
//     .post("send")
//     .request({
//         "FromEmail":"savemeorg@gmail.com",
//         "FromName":"Save Me",
//         "Subject":"Test save me",
//         "Text-part":"Welcome!",
//         "Html-part":"<h3>Dear passenger, welcome to Mailjet!</h3><br />May the delivery force be with you!",
//         "Recipients":[{"Email":"samuelpaulc@gmail.com"}]
//     });
// request
//     .on('success', function (response, body) {
//         console.log (response.statusCode, body);
//     })
//     .on('error', function (err, response) {
//         console.log (response.statusCode, err);
//     });