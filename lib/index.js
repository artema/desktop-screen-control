var platform;

switch (process.platform) {
	case 'win32':
		platform = require('./win32');
		break;
}

module.exports = platform;
