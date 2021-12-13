This .NET program overwrites file create/modify times in .NET. It does so using .NET methods only (no Pinvoke)

#### Get timestamp of file: 

`Timestomp.exe C:\windows\explorer.exe -get`

#### Copy from another file: 

`Timestomp.exe C:\targetfile.exe -copy C:\windows\system32\calc.exe`

#### Set create: 

`Timestomp.exe C:\targetfile.exe -set CreateDate (like YYYY-MM-DDTHH:mm:ss)`

#### Set create and modify: 

`Timestomp.exe C:\targetfile.exe -set CreateDate ModifyDate`

#### Examples: 

`Timestomp.exe C:\targetfile.exe -set 2018-08-18T07:22:16`
`Timestomp.exe C:\targetfile.exe -set 2017-08-18T07:22:16 2018-03-14T03:21:11`