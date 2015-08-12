#desktop-screen-control

Screen brightness and orientation controls for Windows applications.

## Installation

```
npm install desktop-screen-control --save
```

## Supported platforms

* Windows Vista
* Windows 7
* Windows 8 and 8.1

## Requirements

* .NET Framework 4.5

## Usage

```
var platform = require('desktop-screen-control');

if (!platform) {
  return console.log('Platform not supported.');
}

platform.getBrightness(function(error, result) {
  console.log('Brightness: ', result * 100, '%');
});

platform.setBrightness(0.5, function(error, result) {
  if (!error) console.log('Brightness is set to 50%');
});

platform.flip(function(error, result) {
  if (!error) console.log('Screen flipped horizontally.');
});

platform.rotateClockwise(function(error, result) {
  if (!error) console.log('Screen rotated clockwise.');
});

platform.rotateCounterClockwise(function(error, result) {
  if (!error) console.log('Screen rotated counterclockwise.');
});

```
