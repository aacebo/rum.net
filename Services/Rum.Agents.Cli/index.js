#!/usr/bin/env node
const cp = require('node:child_process');
const path = require('node:path');

const cmd = path.join(__dirname, 'bin/Debug/net9.0/Rum.Agents.Cli');
console.log(cmd);

cp.spawnSync(cmd, ['rum', ...process.argv.slice(2)], {
    stdio: 'inherit'
});