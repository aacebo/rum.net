#!/usr/bin/env node
const cp = require('node:child_process');
const path = require('node:path');

const binary = path.join(__dirname, 'bin/Debug/net9.0/Rum.Agents.Cli');
cp.spawnSync(binary, ['rum', ...process.argv.slice(2)], {
    stdio: 'inherit',
    shell: true
});
