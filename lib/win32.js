var edge = require('edge'),
	assembly = './lib/native/win32/DesktopScreenControl.dll';

function getFunc(name) {
	var func = edge.func({ methodName: name, assemblyFile: assembly });
	return function(done) {
		return func('', done);
	};
}

function getAction(name) {
	return edge.func({ methodName: name, assemblyFile: assembly });
}

module.exports = {
	getBrightness: getFunc('GetBrightness'),
	setBrightness: getAction('SetBrightness'),
	flip: getFunc('FlipScreen'),
	rotateClockwise: getFunc('RotateScreenClockwise'),
	rotateCounterClockwise: getFunc('RotateScreenCounterClockwise')
};
