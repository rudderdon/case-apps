# CASE Apps

A revolutionary set of Autodesk Revit add-ins and tools, CASE Apps enhances your daily BIM workflows allowing your teams to do more — better and faster. Previously offered as pro and free subscriptions through [CASE] (http://case-inc.com/) and open sourced by [WeWork](http://wework.com), this software comes with a simple installer. 

While we hope some of our old and new friends continue to actively contribute to these apps on their own - we, as WeWork, will not be managing the GitHub community. We’ve included a description and instructions on how to access the open source code. After that, it’s up to you! 

Huge thanks to [Don Rudder](https://github.com/rudderdon) for developing most of these tools! And let’s not forget [Matthew Nelson](https://github.com/mnelson7982), [Matteo Cominetti](https://github.com/teocomi), and [Kyle Morin](https://github.com/kmorin) for contributing.

## Installation
To install the tools, head over the releases page and download the latest .exe installer: https://github.com/WeConnect/case-apps/releases

## Structure
The code contains 2015 and 2016 verison of the add-in's projects organized in folders by year and set (as per below), these are respectively part of a 2015 and a 2016 solution.
Each project corresponds to a single add-in and will have an Entry namespace that contains the classes to initialize each add-in's command.
The Case.AppsRelease project defines the Revit ribbon layout and buttons, and the add-in manifest is pointing to the Case.AppsRelease.dll.
The installer is a Inno Setup script, it will take all the files into the deploy/ folder and copy them to the respective Revit Add-in folder.

### Building
Post-built events have been added to each .csproj file to copy the built dlls to the deploy/2015/ or deploy/2016/ folder. **Make sure these folders exist otherwise the projects might not build!** You might want to edit post-build debug events to copy the single project's dlls to the addin folder for testing.

## Pull Requests
Feel free to clone/fork the repo and sumbit pull requests to keep improving it!

## Full app list

### Apps Set #1 (former CASE Free Apps)
-	Case.ApplySysOrient
-	Case.BasicReporting
-	Case.ChangeReplaceFamTypeNames
-	Case.DeleteViewsAndPurge
-	Case.Directionality
-	Case.DoorMarkRenumber
-	Case.Export.Families
-	Case.ExportSharedParameters
-	Case.ExtrudeRoomsToMass
-	Case.FamilySubcategories
-	Case.HiddenParameterToParameter
-	Case.ImageToDraftingView
-	Case.LightingLayout
-	Case.LineChanger
-	Case.ModeledRoomTags
-	Case.MultiViewDuplicate
-	Case.ObjectStyles
-	Case.ParallelWalls
-	Case.ReportGroupsByView
-	Case.RevClouds
-	Case.RoomInsertionPoint
-	Case.RoomSync
-	Case.SharedParameters
-	Case.ViewCreator
-	Case.ViewportReporting
-	Case.ViewTemplates


### Apps Set #2 (former CASE Pro Apps)
-	Case.Subs.DeleteViewsAndPurge
-	Case.Subs.Exceler8
-	Case.Subs.Linestyles
-	Case.Subs.MultiViewDuplicate
-	Case.Subs.OpenNURBS
-	Case.Subs.Renamer
-	Case.Subs.RoomsToMass
-	Case.Subs.SharedParameters
-	Case.Subs.SuperTag
-	Case.Subs.ViewSync
-	Case.Subs.ViewTemplates
-	Case.Subs.Worksets
-	Case.Subs.Xyzcopy 

### Apps Set #3 (not included in the installer)
-	Case.DimensionOverrides
-	Case.FreeBenchmarking
-	Case.Navis2BCF (Navisworks Addin)
-	Case.Subs.KeyMatcher
-	Case.UngroupAll
