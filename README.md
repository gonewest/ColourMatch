# Colour Matching API
### A simple api to find the most dominant colour in an image and return its name.
### Written for .net core 2.

# Usage

The api accepts GET or POST requests.

## Example GET

```
{baseurl}/api/colour?url=https://pwintyimages.blob.core.windows.net/samples/stars/sample-black.png
```

## Example POST
```
{baseurl}/api/colour

BODY: "https://pwintyimages.blob.core.windows.net/samples/stars/sample-navy.png"
```

# Configuration
Accepted colour values are defined used RGB values in the appsettings.json

## Example config

```json
  "Colours": [
    { "key": "grey", "red": 123, "green": 123, "blue": 123 },
    { "key": "black", "red": 0, "green": 0, "blue": 0 },
    { "key": "teal", "red": 0, "green": 128, "blue": 128 },
    { "key": "navy", "red": 0, "green": 0, "blue": 128 }
  ]
```
