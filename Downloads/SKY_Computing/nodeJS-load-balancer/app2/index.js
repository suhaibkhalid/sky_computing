const express = require('express');
const app = express();
app.use(express.json());

app.post('/', (req, res) => {
  console.log('App 2 handling request');
  res.json({ source: 'App 2', data: req.body });
});

app.listen(4000, () => console.log('App 2 on 4000'));
