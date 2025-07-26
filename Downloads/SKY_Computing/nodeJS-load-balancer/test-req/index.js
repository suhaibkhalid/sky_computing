const axios = require('axios');

for (let i = 1; i <= 20; i++) {
  //axios.post('http://localhost/', { reqNo: i })
  axios.post('http://13.60.56.58/', { reqNo: i })
    .then(res => {
      console.log(`Request ${i} handled by ${res.data.source}`);
    })
    .catch(err => {
      console.error(`Request ${i} failed`, err.message);
    });
}