const express = require('express');
const app = express();
app.use(express.json());

app.post('/', (req, res) => {
  console.log('App 1 handling request');
  setTimeout(() => {
    res.json({ source: 'App 1', data: req.body });
  }, 3000); // simulate load
});

app.listen(3000, () => console.log('App 1 on 3000'));
