const express = require("express");
const path=require("path");
const app = express();

app.use(express.static(path.join(__dirname, 'public')));

app.get("/", async (req, res) => {
  res.sendFile("index.html",{root:__dirname});
});

app.listen(8000, () => {
  console.log("Server started on http://localhost:8000/");
});
