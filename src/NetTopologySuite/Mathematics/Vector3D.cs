﻿using System;
using System.Data.Common;
using System.Globalization;
using NetTopologySuite.Geometries;

namespace NetTopologySuite.Mathematics
{
    /// <summary>
    /// Represents a vector in 3-dimensional Cartesian space.
    /// </summary>
    /// <author>Martin Davis</author>
    public class Vector3D
    {

        // ReSharper disable InconsistentNaming
        /// <summary>
        /// Computes the dot product of the 3D vectors AB and CD.
        /// </summary>
        /// <param name="A">The start point of the 1st vector</param>
        /// <param name="B">The end point of the 1st vector</param>
        /// <param name="C">The start point of the 2nd vector</param>
        /// <param name="D">The end point of the 2nd vector</param>
        /// <returns>The dot product</returns>
        public static double Dot(Coordinate A, Coordinate B, Coordinate C, Coordinate D)
        {
            double ABx = B.X - A.X;
            double ABy = B.Y - A.Y;
            double ABz = B.Z - A.Z;
            double CDx = D.X - C.X;
            double CDy = D.Y - C.Y;
            double CDz = D.Z - C.Z;
            return ABx * CDx + ABy * CDy + ABz * CDz;
        }
        // ReSharper restore InconsistentNaming

        /// <summary>
        /// Creates a new vector with given <paramref name="x"/>, <paramref name="y"/> and <paramref name="z"/> components.
        /// </summary>
        /// <param name="x">The x component</param>
        /// <param name="y">The y component</param>
        /// <param name="z">The z component</param>
        /// <returns>A new vector</returns>
        public static Vector3D Create(double x, double y, double z)
        {
            return new Vector3D(x, y, z);
        }

        /// <summary>
        /// Creates a vector from a 3D <see cref="Coordinate"/>.
        /// <para/>
        /// The coordinate should have the
        /// X,Y and Z ordinates specified.
        /// </summary>
        /// <param name="coord">The coordinate to copy</param>
        /// <returns>A new vector</returns>
        public static Vector3D Create(Coordinate coord)
        {
            return new Vector3D(coord);
        }


        /// <summary>
        /// Computes the 3D dot-product of two <see cref="Coordinate"/>s
        /// </summary>
        /// <param name="v1">The 1st vector</param>
        /// <param name="v2">The 2nd vector</param>
        /// <returns>The dot product of the (coordinate) vectors</returns>
        public static double Dot(Coordinate v1, Coordinate v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }

        private readonly double _x;
        private readonly double _y;
        private readonly double _z;


        /// <summary>
        /// Creates a new 3D vector from a <see cref="Coordinate"/>.<para/> The coordinate should have
        /// the X,Y and Z ordinates specified.
        /// </summary>
        /// <param name="coord">The coordinate to copy</param>
        public Vector3D(Coordinate coord)
        {
            _x = coord.X;
            _y = coord.Y;
            _z = coord.Z;
        }

        /// <summary>
        /// Creates a vector with the direction and magnitude
        /// of the difference between the <paramref name="to"/>
        /// and <paramref name="from"/> <see cref="Coordinate"/>s.
        /// </summary>
        /// <param name="from">The origin coordinate</param>
        /// <param name="to">The destination coordinate</param>
        public Vector3D(Coordinate from, Coordinate to)
        {
            _x = to.X - from.X;
            _y = to.Y - from.Y;
            _z = to.Z - from.Z;
        }

        /// <summary>
        /// Creates a vector with the given <paramref name="x"/>, <paramref name="y"/> and <paramref name="z"/> components
        /// </summary>
        /// <param name="x">The x component</param>
        /// <param name="y">The y component</param>
        /// <param name="z">The z component</param>
        public Vector3D(double x, double y, double z)
        {
            _x = x;
            _y = y;
            _z = z;
        }

        /// <summary>
        /// Gets a value indicating the x-ordinate
        /// </summary>
        public double X => _x;

        /// <summary>
        /// Gets a value indicating the y-ordinate
        /// </summary>
        public double Y => _y;

        /// <summary>
        /// Gets a value indicating the z-ordinate
        /// </summary>
        public double Z => _z;

        /// <summary>
        /// Computes a vector which is the sum
        /// of this vector and the given vector.
        /// </summary>
        /// <param name="v">The vector to add</param>
        /// <returns>The sum of this and <c>v</c></returns>
        public Vector3D Add(Vector3D v)
        {
            return Create(X + v.X, Y + v.Y, Z + v.Z);
        }

        /// <summary>
        /// Computes a vector which is the difference
        /// of this vector and the given vector.
        /// </summary>
        /// <param name="v">The vector to subtract</param>
        /// <returns>The difference of this and <c>v</c></returns>
        public Vector3D Subtract(Vector3D v)
        {
            return Create(X - v.X, Y - v.Y, Z - v.Z);
        }

        /// <summary>
        /// Creates a new vector which has the same direction
        /// and with length equals to the length of this vector
        /// divided by the scalar value <c>d</c>.
        /// </summary>
        /// <param name="d">The scalar divisor</param>
        /// <returns>A new vector with divided length</returns>
        public Vector3D Divide(double d)
        {
            return Create(_x / d, _y / d, _z / d);
        }

        /// <summary>
        /// Computes the dot-product of this <see cref="Vector3D"/> and <paramref name="v"/>
        /// </summary>
        /// <paramref name="v">The 2nd vector</paramref>
        /// <returns>The dot product of the vectors</returns>
        public double Dot(Vector3D v)
        {
            return _x * v._x + _y * v._y + _z * v._z;
        }


        /// <summary>
        /// Computes the length of this vector
        /// </summary>
        /// <returns>The length of this vector</returns>
        public double Length()
        {
            return Math.Sqrt(_x * _x + _y * _y + _z * _z);
        }

        /// <summary>
        /// Computes the length of vector <paramref name="v"/>.
        /// </summary>
        /// <param name="v">A coordinate representing a 3D Vector</param>
        /// <returns>The length of <paramref name="v"/></returns>
        public static double Length(Coordinate v)
        {
            return Math.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z);
        }

        /// <summary>
        /// Computes a vector having identical direction
        /// but normalized to have length 1.
        /// </summary>
        /// <returns>A new normalized vector</returns>
        public Vector3D Normalize()
        {
            double length = Length();
            if (length > 0.0)
                return Divide(Length());
            return Create(0.0, 0.0, 0.0);
        }

        /// <summary>
        /// Computes a vector having identical direction as <c>v</c>
        /// but normalized to have length 1.
        /// </summary>
        /// <param name="v">A coordinate representing a 3D vector</param>
        /// <returns>A coordinate representing the normalized vector</returns>
        public static Coordinate Normalize(Coordinate v)
        {
            double len = Length(v);
            return new CoordinateZ(v.X / len, v.Y / len, v.Z / len);
        }

        /// <inheritdoc cref="object.ToString()"/>
        public override string ToString()
        {
            return string.Format(NumberFormatInfo.InvariantInfo, "[{0}, {1}, {2}]", _x, _y, _z);
        }

        ///<inheritdoc cref="object.Equals(object)"/>
        public override bool Equals(object o)
        {
            if (!(o is Vector3D v) ) {
                return false;
            }
            return _x == v.X && _y == v.Y && _z == v.Z;
        }

        ///<inheritdoc cref="object.GetHashCode()"/>
        public override int GetHashCode()
        {
            // Algorithm from Effective Java by Joshua Bloch
            int result = 17;
            result = 37 * result + _x.GetHashCode();
            result = 37 * result + _y.GetHashCode();
            result = 37 * result + _z.GetHashCode();
            return result;
        }
    }
}
