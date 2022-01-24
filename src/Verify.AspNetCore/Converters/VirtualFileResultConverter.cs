﻿using Microsoft.AspNetCore.Mvc;

class VirtualFileResultConverter :
    ResultConverter<VirtualFileResult>
{
    protected override void InnerWrite(VerifyJsonWriter writer, VirtualFileResult result)
    {
        FileResultConverter.WriteFileData(writer, result);
        writer.WriteProperty(result, result.FileName, "FileName");
    }
}