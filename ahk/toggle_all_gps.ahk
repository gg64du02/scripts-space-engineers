#NoEnv  ; Recommended for performance and compatibility with future AutoHotkey releases.
; #Warn  ; Enable warnings to assist with detecting common errors.
SendMode Input  ; Recommended for new scripts due to its superior speed and reliability.
SetWorkingDir %A_ScriptDir%  ; Ensures a consistent starting directory.

;use it with stimberries on a phioma to get feces

F1::
	Send {LButton up}
	Send {Down up}
	Reload

MButton::

	test := 0
	
	;en permanence
	While(test<265){
		;delay in ms
		Sleep, 200
		;Sleep, 10
		
		test := test + 1
		
		if(test = 20){
			;msgbox %test%
		}
		;send e
		
		temps := 200
		
		Send {LButton down}
		Sleep %temps%
		Send {LButton up}
		Sleep %temps%
		
		Send {Left down}
		Sleep %temps%
		Send {Left up}
		Sleep %temps%
		
		Send {Up down}
		Sleep %temps%
		Send {Up up}
		Sleep %temps%
		
		Send {Down down}
		Sleep %temps%
		Send {Down up}
		Sleep %temps%
	}
	
Send {Down up}
Sleep %temps%
Send {LButton up}
Sleep %temps%
return