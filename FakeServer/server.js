const http=require("http");
const Path=require("path");
const fs=require("fs");

var server=http.createServer(function (req,res){
    const fileName=Path.resolve(__dirname,"."+req.url);
    const extName=Path.extname(fileName).substr(1);

    //console.log("fileName : "+fileName);
    if (fs.existsSync(fileName) && fs.statSync(fileName).isFile()) {
        var mineTypeMap={
            html:'text/html;charset=utf-8',
            ab:"application/zip"
        }
        if (mineTypeMap[extName]) {
            res.setHeader('Content-Type', mineTypeMap[extName]);
            var stream=fs.createReadStream(fileName);
            stream.pipe(res);
        }
    }
    else
    {
        res.writeHead(404, {"Content-Type": "text/plain"});
        res.write("404 Not Found\n");
        res.end();
        return;
    }

    
})
server.listen(3000);