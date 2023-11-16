# ZPL requests

zpl-zbi2-pm-en.pdf

^XA~JA^XZ

! U1 setvar \"odometer.user_label_count\" \"1\"
! U1 setvar "odometer.user_label_count" "1"

! U1 setvar "media.type" "label"
! U1 setvar "media.sense_mode" "gap"
-- Calibration
~JC^XA^JUS^XZ
~JC - 232 - Set Media Sensor Calibration
^JU - 254 - Configuration Update
The ^JU command sets the active configuration for the printer.
Format:  ^JUa
Parameters Details
a = active configuration Values:
F = reload factory settings
N = reload factory network settings
These values are lost at power-off if not saved with ^JUS.
R = recall last saved settings
S = save current settings
These values are used at power-on.
Default:  a value must be specified
^XA~JA~JC^XZ

^B2 - 053 - Interleaved 2 of 5 Bar Code
^BC - 080 - Code 128 Bar Code (Subsets A, B, and C)
^BY - 134 - Bar Code Field Default
^CI - 140 - Change International Font/Encoding
^CW - 152 - Font Identifier
^FD - 172 - Field Data
^FO - 181 - Field Origin
^FW - 188 - Field Orientation
^FS - 184 - Field Separator
~JA - 229 - Cancel All | Cancels all format commands in the buffer
~JC - 232 - Set Media Sensor Calibration
^JU - 254 - Configuration Update
^LH - 269 - Label Home
^XA - 347 - Start Format | Begin label
^XZ - 352 - End Format | End label
^XG - 350 - Recall Graphic