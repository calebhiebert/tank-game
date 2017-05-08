const PORT = 80;

let express = require('express');
let app = express();
let server = require('http').createServer(app);
let io = require('socket.io')(server);

io.on('connection', skt => {
    console.log('connection ' + skt.id);

    skt.on('test-event', d => {
        console.log(d);
    });

    skt.on('latency', d => {
        skt.emit('latency');
        console.log('latency calc');
    })
});

server.listen(PORT);