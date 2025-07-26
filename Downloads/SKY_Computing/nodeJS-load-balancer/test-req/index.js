const axios = require('axios');

for (let i = 1; i <= 20; i++) {
  axios.post('http://localhost/', { reqNo: i })
    .then(res => {
      console.log(`Request ${i} handled by ${res.data.source}`);
    })
    .catch(err => {
      console.error(`Request ${i} failed`, err.message);
    });
}
