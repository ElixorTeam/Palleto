# TSC cmd
- TSC_TSPL_TSPL2_Programming.pdf

## Cmd
```
SPEED 3
DENSITY 10
DIRECTION 1
SIZE 4.0,2.0
GAP 0.00,0.0
CLS
PUTBMP 10,10,"REC5.BMP"
PRINT 1
```

## Cmd
```
SPEED 3
DENSITY 10
DIRECTION 1
SIZE 100 mm, 80.00 mm
GAP 3.50 mm, 0.00 mm
CLS
PUTPCX 200,888,"EAC.BMP"
PUTBMP 315,888,"FISH.BMP"
PUTBMP 435,888,"TEMP6.BMP"
PRINT 1
```

## Cmd
```
DIRECTION 1
CLS
PUTBMP 200,888,"EAC.BMP",8,80
PUTBMP 315,888,"FISH.BMP",8,80
PUTBMP 435,888,"TEMP6.BMP",8,80
PRINT 1
```

## Cmd
```
SIZE - defines the label width and length
GAP - gap distance between two labels
	GAP 3.50 mm, 0.00 mm
GAPDETECT - calibrate
BLINEDETECT - 
AUTODETECT - 
BLINE - sets the height of the black line and the user-defined extra label feeding length each form feed takes
OFFSET - 
SPEED - defines the print speed
DENSITY - sets the printing darkness: 0 - 15
DIRECTION - defines the printout direction and mirror image
REFERENCE - defines the reference point of the label
SHIFT - moves the label�s vertical position
COUNTRY - orients the keyboard for use in different countries via defining special characters
CODEPAGE - defines the code page of international character set: 866 / UTF-8
CLS - clears the image buffer
FEED - feeds label with the specified length
BACKFEED & BACKUP - feeds the label in reverse. The length is specified by dot
FORMFEED - feeds label to the beginning of next label
HOME - feed label until the internal sensor has determined the origin. Size and gap of the label should be defined before using this command
PRINT - prints the label format currently stored in the image buffer
	PRINT 1,1
SOUND - controls the sound frequency of the beeper. There are 10 levels of sounds. The timing control can be set by the "interval" parameter. 
	SOUND 5,200
CUT - activates the cutter to immediately cut the labels without back feeding the label
LIMITFEED - If the gap sensor is not set to a suitable sensitivity while feeding labels, the printer will not be able to 
	locate the correct position of the gap. This command stops label feeding and makes the red LED flash if 
	the printer does not locate gap after feeding the length of one label plus one preset value. 
SELFTEST - print out the printer information
EOJ - Let the printer wait until process of commands (before EOJ) be finished then go on the next command
DELAY - Let the printer wait specific period of time then go on next command
DISPLAY - show the image, which is in printer�s image buffer, on LCD panel
INITIALPRINTER -  restore printer settings to defaults
BAR - draws a bar on the label format
BARCODE - prints 1D barcodes
TLC39 - draws TLC39, TCIF Linked Bar Code 3 of 9, barcode
BITMAP - draws bitmap images (as opposed to BMP graphic files)
BOX - draws rectangles on the label
CIRCLE - draws a circle on the label
ELLIPSE - draws an ellipse on the label
CODABLOCK F mode - draws CODABLOCK F mode barcode
DMATRIX - defines a DataMatrix 2D bar code. Currently, only ECC200 error correction is supported
ERASE - clears a specified region in the image buffer
MAXICODE - defines a 2D Maxicode
PDF417 - defines a PDF417 2D bar code
AZTEC - defines a AZTEC 2D bar code
MPDF417 - defines a Micro PDF 417 bar code
PUTBMP - prints BMP format images. The grayscale printing is for direct thermal mode only
	Support 1-bit (monochrome) and 8-bit (256-color) BMP graphic only
PUTPCX - prints PCX format images. TSPL language supports 2-color PCX format graphics. TSPL2 language supports 256-color PCX format graphics
QRCODE - prints QR code
RSS - draw a RSS bar code on the label format
REVERSE - reverses a region in image buffer
	REVERSE x_start,y_start,x_width,y_height
	REVERSE 90,90,128,40
TEXT - prints text on label
BLOCK - prints paragraph on label
<ESC>!? - obtains the printer status at any time, even in the event of printer error
<ESC>!C - restarts the printer and omits to run AUTO.BAS. The beginning of the command is an ESCAPE character (ASCII 27). 
<ESC>!D - to disable immediate command, ex. <ESC>!R <RSC>!? <ESC>!C and so on, which is starting by <ESC>!. 
	The beginning of the command is an ESCAPE character (ASCII 27)
<ESC>!O - cancel the PAUSE status of printer
<ESC>!P - PAUSE the printer
<ESC>!Q - restarts the printer and omits to run AUTO.BAS. The beginning of the command is an ESCAPE character (ASCII 27)
<ESC>!R - resets the printer. The beginning of the command is an ESCAPE character (ASCII 27)
<ESC>!S - obtains the printer status at any time, even in the event of printer error
<ESC>!F - using to feed a label. This function is the same as to press the FEED button
<ESC>!. - cancel all printing files
~!@ - inquires the mileage of the printer
SET GAP - 
	SET GAP n/AUTO/OFF/0,/REVERSE/OBVERSE
	SET GAP 6,
```