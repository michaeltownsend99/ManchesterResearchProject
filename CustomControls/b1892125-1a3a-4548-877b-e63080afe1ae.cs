// Copyright MyScript. All rights reserved.

namespace MyScript.Certificate
{

  /// <summary>
  /// The <c>MyCertificate</c> class provides the bytes of the user
  /// certificate used to grant the access to the MyScript technologies.
  /// </summary>
  public sealed class MyCertificate
  {
    /// <summary>
    /// Read only property that returns the bytes of the user certificate.
    /// </summary>
    ///
    /// <value>
    /// The bytes of the user certificate.
    /// </value>
    public static sbyte[] Bytes
    {
      get
      {
          return BYTES;
      }
    }

    /// <summary>
    /// The bytes of the user certificate.
    /// </summary>
    private static readonly sbyte[] BYTES = new sbyte[]
    {
      -95,  -99,  -66,  -60,  -57,  0,    -120, 114,  
      -58,  -59,  9,    -77,  42,   115,  -56,  -122, 
      -27,  -109, -32,  46,   -74,  -110, -1,   59,   
      -28,  -117, 124,  44,   53,   -51,  87,   69,   
      -38,  100,  126,  59,   10,   102,  112,  -10,  
      61,   -44,  32,   -108, -92,  65,   -59,  71,   
      -16,  109,  -5,   2,    31,   -122, -36,  53,   
      65,   -17,  -74,  110,  -75,  -50,  -121, 20,   
      -29,  -18,  37,   84,   -73,  -98,  94,   -92,  
      -114, 81,   -52,  91,   -102, 12,   -24,  29,   
      72,   70,   -128, 23,   83,   107,  -8,   4,    
      -32,  65,   -6,   -22,  -25,  -85,  94,   6,    
      -65,  -55,  99,   -41,  -34,  108,  -89,  25,   
      81,   -122, -9,   118,  -87,  -95,  80,   15,   
      -5,   4,    115,  55,   100,  53,   -15,  -124, 
      53,   -82,  -101, 7,    -37,  71,   -26,  -62,  
      -112, -117, 26,   81,   -63,  74,   22,   26,   
      -123, 34,   38,   -56,  21,   -53,  93,   -92,  
      125,  18,   -81,  31,   55,   -85,  42,   76,   
      -119, 74,   109,  -3,   -100, 111,  -77,  -104, 
      63,   -75,  -63,  91,   112,  -125, -97,  121,  
      25,   65,   -96,  19,   -16,  -38,  -91,  -123, 
      56,   -8,   61,   35,   121,  -89,  -2,   13,   
      -17,  -58,  26,   31,   21,   -51,  94,   80,   
      -104, 40,   124,  -42,  7,    98,   28,   62,   
      16,   72,   64,   -11,  2,    49,   -78,  -3,   
      84,   38,   -66,  -104, -103, -117, 81,   84,   
      31,   -118, -36,  -34,  34,   119,  -108, 55,   
      -63,  70,   121,  -124, -32,  -94,  -49,  -75,  
      -25,  5,    24,   65,   98,   80,   -107, -52,  
      -122, 93,   33,   58,   -53,  -92,  62,   83,   
      91,   -12,  -117, 114,  -71,  -52,  -55,  -35,  
      34,   -89,  109,  30,   -82,  72,   28,   65,   
      124,  83,   -42,  86,   -102, -76,  55,   -126, 
      70,   -91,  111,  30,   68,   18,   -117, 66,   
      -80,  40,   69,   85,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      103,  -34,  -15,  86,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  -101, -76,  55,   -126, 
      39,   70,   -78,  28,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      56,   -91,  111,  30,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      -36,  90,   -112, -31,  68,   18,   -117, 66,   
      -64,  21,   101,  87,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      103,  -34,  -15,  86,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  -101, -76,  55,   -126, 
      -9,   -39,  90,   27,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      56,   -91,  111,  30,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      -36,  90,   -112, -31,  68,   18,   -117, 66,   
      -6,   -70,  -103, 84,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      103,  -34,  -15,  86,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  -101, -76,  55,   -126, 
      -107, -28,  -47,  27,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      56,   -91,  111,  30,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      -36,  90,   -112, -31,  68,   18,   -117, 66,   
      96,   -120, 70,   82,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      103,  -34,  -15,  86,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  -101, -76,  55,   -126, 
      18,   41,   -26,  31,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      56,   -91,  111,  30,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      -36,  90,   -112, -31,  71,   18,   -117, 66,   
      125,  32,   18,   83,   70,   -69,  55,   -126, 
      33,   -91,  111,  30,   -70,  -19,  116,  -67,  
      103,  -34,  -15,  86,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  -104, -76,  55,   -126, 
      70,   -97,  107,  28,   -101, 29,   -117, 66,   
      120,  -34,  -15,  86,   101,  75,   -56,  125,  
      56,   -91,  111,  30,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      -36,  90,   -112, -31,  71,   18,   -117, 66,   
      -100, -34,  5,    85,   120,  -69,  55,   -126, 
      38,   -91,  111,  30,   -70,  -19,  116,  -67,  
      103,  -34,  -15,  86,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  -104, -76,  55,   -126, 
      41,   61,   -116, 28,   -94,  29,   -117, 66,   
      120,  -34,  -15,  86,   101,  75,   -56,  125,  
      56,   -91,  111,  30,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      -36,  90,   -112, -31,  71,   18,   -117, 66,   
      39,   -119, -112, 87,   113,  -69,  55,   -126, 
      42,   -91,  111,  30,   -70,  -19,  116,  -67,  
      103,  -34,  -15,  86,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  -104, -76,  55,   -126, 
      -13,  -109, -39,  29,   -79,  29,   -117, 66,   
      118,  -34,  -15,  86,   101,  75,   -56,  125,  
      56,   -91,  111,  30,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      -36,  90,   -112, -31,  71,   18,   -117, 66,   
      4,    108,  39,   82,   100,  -69,  55,   -126, 
      43,   -91,  111,  30,   -70,  -19,  116,  -67,  
      103,  -34,  -15,  86,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  -104, -76,  55,   -126, 
      -67,  -43,  104,  31,   67,   2,    -117, 66,   
      123,  -34,  -15,  86,   101,  75,   -56,  125,  
      56,   -91,  111,  30,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      -36,  90,   -112, -31,  71,   18,   -117, 66,   
      101,  -99,  -65,  87,   113,  -69,  55,   -126, 
      32,   -91,  111,  30,   -70,  -19,  116,  -67,  
      103,  -34,  -15,  86,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  -104, -76,  55,   -126, 
      -101, -47,  19,   27,   72,   2,    -117, 66,   
      116,  -34,  -15,  86,   101,  75,   -56,  125,  
      56,   -91,  111,  30,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      -36,  90,   -112, -31,  65,   18,   -117, 66,   
      33,   -119, 4,    82,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      103,  -34,  -15,  86,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  -98,  -76,  55,   -126, 
      -55,  8,    -96,  29,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      56,   -91,  111,  30,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      -36,  90,   -112, -31,  65,   18,   -117, 66,   
      -87,  -15,  -80,  85,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      103,  -34,  -15,  86,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  -98,  -76,  55,   -126, 
      111,  78,   114,  29,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      56,   -91,  111,  30,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      -36,  90,   -112, -31,  70,   18,   -117, 66,   
      102,  -99,  41,   82,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      103,  -34,  -15,  86,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  -103, -76,  55,   -126, 
      74,   0,    28,   31,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      56,   -91,  111,  30,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      -36,  90,   -112, -31,  70,   18,   -117, 66,   
      113,  -38,  5,    85,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      103,  -34,  -15,  86,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  -103, -76,  55,   -126, 
      92,   -128, -55,  28,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      56,   -91,  111,  30,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      -36,  90,   -112, -31,  70,   18,   -117, 66,   
      94,   -68,  -110, 82,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      103,  -34,  -15,  86,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  -103, -76,  55,   -126, 
      -88,  38,   -41,  30,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      56,   -91,  111,  30,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      -36,  90,   -112, -31,  70,   18,   -117, 66,   
      87,   -57,  23,   86,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      103,  -34,  -15,  86,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  -103, -76,  55,   -126, 
      117,  118,  -104, 30,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      56,   -91,  111,  30,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      -36,  90,   -112, -31,  70,   18,   -117, 66,   
      58,   -44,  -40,  87,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      103,  -34,  -15,  86,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  -103, -76,  55,   -126, 
      -111, 51,   66,   31,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      56,   -91,  111,  30,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      -36,  90,   -112, -31,  70,   18,   -117, 66,   
      20,   -44,  120,  87,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      103,  -34,  -15,  86,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  -103, -76,  55,   -126, 
      3,    66,   -99,  31,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      56,   -91,  111,  30,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      -36,  90,   -112, -31,  70,   18,   -117, 66,   
      -2,   -49,  9,    87,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      103,  -34,  -15,  86,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  -103, -76,  55,   -126, 
      107,  20,   95,   28,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      56,   -91,  111,  30,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      -36,  90,   -112, -31,  70,   18,   -117, 66,   
      -12,  119,  -61,  84,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      103,  -34,  -15,  86,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  -103, -76,  55,   -126, 
      124,  126,  51,   28,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      56,   -91,  111,  30,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      -36,  90,   -112, -31,  70,   18,   -117, 66,   
      -115, -76,  -82,  84,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      103,  -34,  -15,  86,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  -103, -76,  55,   -126, 
      26,   59,   -61,  28,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      56,   -91,  111,  30,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      -36,  90,   -112, -31,  70,   18,   -117, 66,   
      78,   8,    45,   84,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      103,  -34,  -15,  86,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  -103, -76,  55,   -126, 
      7,    -72,  110,  29,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      56,   -91,  111,  30,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      -36,  90,   -112, -31,  70,   18,   -117, 66,   
      48,   -47,  -111, 85,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      103,  -34,  -15,  86,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  -103, -76,  55,   -126, 
      -31,  21,   10,   29,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      56,   -91,  111,  30,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      -36,  90,   -112, -31,  70,   18,   -117, 66,   
      -43,  54,   -105, 85,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      103,  -34,  -15,  86,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  -103, -76,  55,   -126, 
      -117, -1,   2,    29,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      56,   -91,  111,  30,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      -36,  90,   -112, -31,  70,   18,   -117, 66,   
      -113, -110, 116,  85,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      103,  -34,  -15,  86,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  -103, -76,  55,   -126, 
      36,   10,   -59,  29,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      56,   -91,  111,  30,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      -36,  90,   -112, -31,  70,   18,   -117, 66,   
      105,  52,   92,   85,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      103,  -34,  -15,  86,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  -103, -76,  55,   -126, 
      51,   72,   -42,  29,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      56,   -91,  111,  30,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      -36,  90,   -112, -31,  70,   18,   -117, 66,   
      -114, -74,  48,   85,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      103,  -34,  -15,  86,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  -103, -76,  55,   -126, 
      31,   110,  -119, 29,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      56,   -91,  111,  30,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      -36,  90,   -112, -31,  70,   18,   -117, 66,   
      51,   88,   25,   85,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      103,  -34,  -15,  86,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  -103, -76,  55,   -126, 
      -101, 75,   -112, 29,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      56,   -91,  111,  30,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      -36,  90,   -112, -31,  70,   18,   -117, 66,   
      -121, -28,  -14,  82,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      103,  -34,  -15,  86,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  -103, -76,  55,   -126, 
      -81,  72,   102,  26,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      56,   -91,  111,  30,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      -36,  90,   -112, -31,  70,   18,   -117, 66,   
      6,    -89,  -90,  82,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      103,  -34,  -15,  86,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  -103, -76,  55,   -126, 
      29,   -88,  54,   26,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      56,   -91,  111,  30,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      -36,  90,   -112, -31,  70,   18,   -117, 66,   
      -52,  76,   -114, 82,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      103,  -34,  -15,  86,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  -103, -76,  55,   -126, 
      -116, 16,   -5,   26,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      56,   -91,  111,  30,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      -36,  90,   -112, -31,  70,   18,   -117, 66,   
      -27,  -99,  102,  82,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      103,  -34,  -15,  86,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  -103, -76,  55,   -126, 
      -58,  34,   -10,  26,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      56,   -91,  111,  30,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      -36,  90,   -112, -31,  70,   18,   -117, 66,   
      -3,   29,   89,   82,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      103,  -34,  -15,  86,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  -103, -76,  55,   -126, 
      75,   9,    -35,  26,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      56,   -91,  111,  30,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      -36,  90,   -112, -31,  70,   18,   -117, 66,   
      53,   125,  57,   82,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      103,  -34,  -15,  86,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  -103, -76,  55,   -126, 
      -6,   -19,  -75,  26,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      56,   -91,  111,  30,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      -36,  90,   -112, -31,  70,   18,   -117, 66,   
      79,   35,   46,   82,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      103,  -34,  -15,  86,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  -103, -76,  55,   -126, 
      60,   -101, -101, 26,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      56,   -91,  111,  30,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      -36,  90,   -112, -31,  70,   18,   -117, 66,   
      55,   -39,  -13,  83,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      103,  -34,  -15,  86,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  -103, -76,  55,   -126, 
      82,   -94,  100,  27,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      56,   -91,  111,  30,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      -36,  90,   -112, -31,  70,   18,   -117, 66,   
      -53,  29,   -4,   83,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      103,  -34,  -15,  86,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  -103, -76,  55,   -126, 
      24,   32,   90,   27,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      56,   -91,  111,  30,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      -36,  90,   -112, -31,  70,   18,   -117, 66,   
      13,   57,   -87,  83,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      103,  -34,  -15,  86,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  -103, -76,  55,   -126, 
      -123, 67,   53,   27,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      56,   -91,  111,  30,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      -36,  90,   -112, -31,  70,   18,   -117, 66,   
      -25,  67,   -103, 83,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      103,  -34,  -15,  86,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  -103, -76,  55,   -126, 
      -22,  -15,  2,    27,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      56,   -91,  111,  30,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      -36,  90,   -112, -31,  70,   18,   -117, 66,   
      41,   127,  -126, 83,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      103,  -34,  -15,  86,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  -103, -76,  55,   -126, 
      -72,  -47,  27,   27,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      56,   -91,  111,  30,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      -36,  90,   -112, -31,  70,   18,   -117, 66,   
      -113, -63,  124,  83,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      103,  -34,  -15,  86,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  -103, -76,  55,   -126, 
      -10,  -29,  -4,   27,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      56,   -91,  111,  30,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      -36,  90,   -112, -31,  70,   18,   -117, 66,   
      -1,   9,    79,   83,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      103,  -34,  -15,  86,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  -103, -76,  55,   -126, 
      83,   2,    -70,  27,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      56,   -91,  111,  30,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      -36,  90,   -112, -31,  70,   18,   -117, 66,   
      13,   -120, 9,    84,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      103,  -34,  -15,  86,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  -103, -76,  55,   -126, 
      62,   26,   81,   29,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      56,   -91,  111,  30,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      -36,  90,   -112, -31,  70,   18,   -117, 66,   
      -3,   79,   64,   86,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      103,  -34,  -15,  86,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  -103, -76,  55,   -126, 
      80,   54,   93,   27,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      56,   -91,  111,  30,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      -36,  90,   -112, -31,  70,   18,   -117, 66,   
      -64,  -107, -83,  85,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      103,  -34,  -15,  86,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  -103, -76,  55,   -126, 
      -110, 127,  7,    28,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      56,   -91,  111,  30,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      -36,  90,   -112, -31,  70,   18,   -117, 66,   
      -115, 62,   85,   84,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      103,  -34,  -15,  86,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  -103, -76,  55,   -126, 
      32,   109,  101,  26,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      56,   -91,  111,  30,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      -36,  90,   -112, -31,  70,   18,   -117, 66,   
      -103, -105, -126, 87,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      103,  -34,  -15,  86,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  -103, -76,  55,   -126, 
      -30,  88,   -76,  31,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      56,   -91,  111,  30,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      -36,  90,   -112, -31,  70,   18,   -117, 66,   
      43,   -61,  41,   84,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      103,  -34,  -15,  86,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  -103, -76,  55,   -126, 
      17,   42,   5,    31,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      56,   -91,  111,  30,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      -36,  90,   -112, -31,  70,   18,   -117, 66,   
      51,   49,   31,   82,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      103,  -34,  -15,  86,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  -103, -76,  55,   -126, 
      -56,  -20,  -94,  26,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      56,   -91,  111,  30,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      -36,  90,   -112, -31,  70,   18,   -117, 66,   
      -23,  -22,  -116, 87,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      103,  -34,  -15,  86,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  -103, -76,  55,   -126, 
      -91,  107,  3,    31,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      56,   -91,  111,  30,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      -36,  90,   -112, -31,  70,   18,   -117, 66,   
      -25,  18,   -96,  83,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      103,  -34,  -15,  86,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  -97,  -76,  55,   -126, 
      35,   -91,  111,  30,   80,   2,    -117, 66,   
      78,   -33,  -15,  86,   101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      -36,  90,   -112, -31,  67,   18,   -117, 66,   
      -125, 33,   14,   -87,  -35,  -91,  55,   -126, 
      19,   -91,  111,  30,   -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  101,  75,   -56,  125,  
      -36,  90,   -112, -31,  -70,  -19,  116,  -67,  
      -125, 33,   14,   -87,  -88,  -16,  122,  -29,  
      87,   -51,  60,   118,  36,   98,   -18,  22,   
      25,   -90,  -123, 31,   -12,  -33,  100,  -25,  
      66,   -41,  12,   118,  21,   96,   -18,  38,   
      21,   -67,  -123, 63,   -11,  -38,  115,  -19,  
      64,   -48,  2,    123,  43,   102,  -52,  39,   
      15,   -86,  -124, 36,   -1,   -11,  89,   -29,  
      79,   -36,  21,   123,  55,   73,   -96,  -128, 
      5,    9,    -26,  -82,  -102, -112, 54,   -14,  
      -89,  -107, -19,  31,   103,  34,   -122, 68,   
      117,  -12,  119,  30,   28,   67,   58,   -125, 
      34,   -92,  106,  30,   70,   -112, -118, 77,   
      124,  -18,  115,  87,   -112, -74,  -75,  -125, 
      34,   -91,  -80,  -26,  123,  13,   24,   -51,  
      -60,  -16,  -126, 120,  -19,  99,   -98,  -44,  
      53,   -43,  -103, 87,   -3,   -48,  -46,  -14,  
      -37,  -35,  0,    -54,  56,   -38,  16,   -74,  
      37,   -31,  108,  -100, -59,  104,  -124, -127, 
      113,  92,   -52,  17,   -113, -53,  -72,  -91,  
      -11,  -25,  -25,  37,   20,   108,  -26,  69,   
      77,   117,  79,   -77,  120,  -80,  105,  -12,  
      82,   -7,   3,    -24,  103,  -111, -56,  123,  
      -37,  -46,  -122, -118, 2,    85,   22,   45,   
      -115, -84,  -100, -57,  35,   102,  83,   58,   
      -42,  -19,  -122, -42,  6,    46,   94,   -18,  
      87,   27,   -9,   97,   -35,  26,   25,   -1,   
      113,  -118, -29,  110,  -40,  49,   -12,  112,  
      -51,  85,   -112, 3,    49,   97,   61,   -113, 
      -13,  6,    24,   116,  -13,  -76,  -15,  122,  
      15,   -101, 46,   -84,  -50,  111,  34,   -57,  
      -114, -30,  64,   -62,  70,   -45,  -30,  4,    
      51,   77,   -60,  101,  -100, -26,  124,  -74,  
      -22,  -127, 19,   16,   112,  -116, 84,   -108, 
      -65,  101,  106,  36,   -100, 29,   -68,  81,   
      -35,  75,   18,   46,   65,   110,  93,   42,   
      -127, 55,   96,   -114, -11,  94,   -89,  36,   
      32,   -83,  -96,  66,   95,   -113, 56,   -54,  
      -96,  51,   59,   -83,  108,  -21,  4,    -76,  
      -63,  87,   25,   -116, 107,  -31,  -50,  29,   
      61,   78,   -81,  22,   -83,  65,   98,   -56,  
      -51,  94,   26,   -33,  -32,  1,    -79,  -65,  
      54,   -12,  122,  69,   -92,  -54,  24,   126,  
      2,    -127, 57,   33,   125,  30,   -91,  35,   
      68,   79,   62,   -46,  70,   114,  -67,  20,   
      -32,  -91,  45,   1,    -43,  54,   103,  54,   
      -69,  -64,  109,  29,   68,   18,   -118, 42,   
      8,    -86,  -127, 37,   -96,  -101, 24,   -29,  
      87,   -50,  65,   115,  60,   97,   -24,  48,   
      21,   -82,  -123, 120,  -7,   -37,  90,   -83,  
      71,   -64,  25,   119,  38,   119,  -76,  35,   
      12,   -82,  -72,  18,   -89,  -111, 68,   -92,  
      71,   -60,  27,   127,  120,  55,   -8    
    };
  };
}
