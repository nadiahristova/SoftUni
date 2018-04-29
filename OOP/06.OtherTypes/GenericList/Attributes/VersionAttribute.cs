using System;

[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class 
    | AttributeTargets.Interface | AttributeTargets.Enum 
    | AttributeTargets.Method,  AllowMultiple = false)]
public class VersionAttribute : System.Attribute
{
    public int major;
    public int minor;

    public VersionAttribute(int major, int minor)
    {
        this.Major = major;
        this.Minor = minor;
    }


    public int Major
    {
        get { return this.major; }
        private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Version cannot be negative number.");

                this.major = value;
            }
    }

    public int Minor
    {
        get { return this.minor; }
        private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Version cannot be negative number.");

                this.minor = value;
            }
    }
}
