
using System;

public enum EventCode
{
    NewPlayer,
    SyncPosition,

}

public enum OperationCode//请求响应
{
    Login,
    Reister,
    SyncPosition,
    SyncPlayer,
    Default
}

public enum ParameterCode
{
    Username,
    Password,
    Position,
    UsernameList,
    UserDataList,

    x, y,z
}

public enum ReturnCode//
{
    Success,
    Fail
}


public class Vector3Data
{
    public float x;
    public float y;
    public float z;
}

public class Constant
{
    public const string Horizontal = "Horizontal";
    public const string Vertical = "Vertical";
}

public class PlayerData
{
    public Vector3Data Pos { get; set; }
    public string Username { get; set; }
}


