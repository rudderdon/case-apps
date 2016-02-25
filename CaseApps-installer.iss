#define MyAppName "CASE Apps"
#define MyAppVersion "15.4.8"
#define MyAppPublisher "CASE"
#define MyAppURL "https://github.com/WeConnect/case-apps"

#define RevitAppName  "Case.Apps"
#define RevitAddinFolder "{userappdata}\Autodesk\REVIT\Addins"
#define RevitFolder15 RevitAddinFolder+"\2015\"+RevitAppName
#define RevitAddin15  RevitAddinFolder+"\2015\"
#define RevitFolder16 RevitAddinFolder+"\2016\"+RevitAppName
#define RevitAddin16  RevitAddinFolder+"\2016\"

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{1AE796B6-5B97-4CC7-848A-C3F44956FB90}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={pf}\{#MyAppName}
DisableDirPage=yes
DefaultGroupName=CASE Design, Inc\{#MyAppName}
DisableProgramGroupPage=yes
LicenseFile=.\LICENSE
OutputDir=.
OutputBaseFilename=CASEApps-installer
SetupIconFile=assets\logo.ico
WizardImageFile=assets\banner.bmp
Compression=lzma
SolidCompression=yes
;info: http://revolution.screenstepslive.com/s/revolution/m/10695/l/95041-signing-installers-you-create-with-inno-setup
;comment/edit the line below if you are not signing the exe with the CASE pfx
SignTool=signtoolcase

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Components]
Name: revit15; Description: CASE Apps for Autodesk Revit 2015; Types: full 
Name: revit16; Description: CASE Apps for Autodesk Revit 2016;  Types: full

[Files]
; REVIT 2015 ~~~~~~~~~~~~~~~~~~~
Source: "deploy\2015\*"; DestDir: "{#RevitFolder15}"; Excludes: "*.pdb,*.xml,*.config,*.addin,*.tmp"; Flags: ignoreversion recursesubdirs createallsubdirs; Components: revit15 
Source: "deploy\{#RevitAppName}.addin"; DestDir: "{#RevitAddin15}"; Flags: ignoreversion; Components: revit15 

;REVIT 2016 ~~~~~~~~~~~~~~~~~~~
Source: "deploy\2016\*"; DestDir: "{#RevitFolder16}"; Excludes: "*.pdb,*.xml,*.config,*.addin,*.tmp"; Flags: ignoreversion recursesubdirs createallsubdirs; Components: revit16 
Source: "deploy\{#RevitAppName}.addin"; DestDir: "{#RevitAddin16}"; Flags: ignoreversion; Components: revit16

; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\{cm:ProgramOnTheWeb,{#MyAppName}}"; Filename: "{#MyAppURL}"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"
