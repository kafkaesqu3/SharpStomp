This .NET program overwrites file create/modify times in .NET. It does so using .NET methods only (no Pinvoke)

#### Get timestamp of file: 

`Timestomp.exe C:\windows\explorer.exe -get`

#### Copy from another file: 

`Timestomp.exe C:\targetfile.exe -copy C:\windows\system32\calc.exe`

#### Set create and modify to the same timestamp: 

`Timestomp.exe C:\targetfile.exe -set Date (like YYYY-MM-DDTHH:mm:ss)`

`Timestomp.exe C:\targetfile.exe -set 2018-08-18T07:22:16`


#### Set create and modify to different timestamps: 

`Timestomp.exe C:\targetfile.exe -set CreateDate ModifyDate`

`Timestomp.exe C:\targetfile.exe -set 2017-08-18T07:22:16 2018-03-14T03:21:11`