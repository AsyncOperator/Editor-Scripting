using UnityEngine;

namespace Extensions {
    public static class Extensions {
        public static Vector3 X( this Vector3 v, float value ) {
            return new Vector3( value, v.y, v.z );
        }

        public static Vector3 Y( this Vector3 v, float value ) {
            return new Vector3( v.x, value, v.z );
        }

        public static Vector3 Z( this Vector3 v, float value ) {
            return new Vector3( v.x, v.y, value );
        }
    }
}