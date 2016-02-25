# CASE Apps

## Intro

As CASE decided to join WeWork, a whole new opportunity to change and improve the way we build began, it empowered us to take their work to the next level. Teams started focusing completely on expanding and enhancing the already impressive physical and digital platforms that enables WeWork members to create incredible things.

As a first step of this Building Information revolution, we are extremely excited to announce the Open Sourcing of all CASE apps, the Building Analytics platform and Issue Tracking system.

## Description

The CASE Apps are a revolutionary set of Autodesk Revit Add-ins and Tools that enhance your daily BIM workflows, allowing your teams to do more, do it better and faster. 
These apps were originally organized as Pro and Free, but now they are all open source, and come with a simple installer. 

Big ups to Don Rudder for developing most of these tools! But also to Matthew Nelson, Matteo Cominetti and Kyle Morin.

For more info on each tool, visit the old CASE Apps page: http://apps.case-inc.com/

## Installation
To install the tools, head over the releases page and download the latest .exe installer: https://github.com/WeConnect/case-apps/releases

## Structure
The code contains 2015 and 2016 verison of the addin's projects organized in folders by year and set (as per below), these are respectively part of a 2015 and a 2016 solution.
Each project corresponds to a single addin and will have an Entry namespace that contains the classes to initialize each addin's command.
The Case.AppsRelease project defines the Revit ribbon layout and buttons, and the addin manifest is pointing to the Case.AppsRelease.dll.
The installer is a Inno Setup script, it will take all the files into the deploy/ folder and copy them to the respective Revit Addin folder.

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
