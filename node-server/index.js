const PORT = 80;

let express = require('express');
let app = express();
let server = require('http').createServer(app);
let io = require('socket.io')(server);

io.on('connection', skt => {
    console.log('connection ' + skt.id);

    skt.on('position-update', d => {
        //console.log(skt.id + ' ' + JSON.stringify(d));
        io.emit('position-update', {id: skt.id, x: d.x, y: d.y, r: d.r});
    });

    skt.on('disconnect', d => {
        console.log('disconnect ' + skt.id);
        io.emit('player-disconnect', {id: skt.id});
    });
});

server.listen(PORT);