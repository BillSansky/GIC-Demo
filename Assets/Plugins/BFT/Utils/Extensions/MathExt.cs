using System;
using System.Collections.Generic;
using UnityEngine;

namespace BFT
{
    public enum AxisBFT
    {
        RIGHT,
        UP,
        FORWARD,
        LEFT,
        DOWN,
        BACK
    }

    public static class MathExt
    {
        private static UnityEngine.Transform tempChild;
        private static UnityEngine.Transform tempParent;

        private static Vector3[] positionRegister;
        private static float[] posTimeRegister;
        private static int positionSamplesTaken;

        private static float zeroPrecision = 0.0000001f;

        public static bool IsAxisNegative(AxisBFT axis)
        {
            switch (axis)
            {
                case AxisBFT.RIGHT:
                case AxisBFT.UP:
                case AxisBFT.FORWARD:
                    return false;
                case AxisBFT.LEFT:
                case AxisBFT.DOWN:
                case AxisBFT.BACK:
                    return true;
                default:
                    throw new ArgumentOutOfRangeException("axis", axis, null);
            }
        }

        public static Vector3 Axis(AxisBFT axis)
        {
            switch (axis)
            {
                case AxisBFT.RIGHT:
                    return Vector3.right;
                case AxisBFT.UP:
                    return Vector3.up;
                case AxisBFT.FORWARD:
                    return Vector3.forward;
                case AxisBFT.LEFT:
                    return Vector3.left;
                case AxisBFT.DOWN:
                    return Vector3.down;
                case AxisBFT.BACK:
                    return Vector3.back;
                default:
                    throw new ArgumentOutOfRangeException("axis", axis, null);
            }
        }

        public static AxisBFT GetOpposedAxis(AxisBFT axis)
        {
            switch (axis)
            {
                case AxisBFT.RIGHT:
                    return AxisBFT.LEFT;
                case AxisBFT.UP:
                    return AxisBFT.DOWN;
                case AxisBFT.FORWARD:
                    return AxisBFT.BACK;
                case AxisBFT.LEFT:
                    return AxisBFT.RIGHT;
                case AxisBFT.DOWN:
                    return AxisBFT.UP;
                case AxisBFT.BACK:
                    return AxisBFT.FORWARD;
                default:
                    throw new ArgumentOutOfRangeException("axis", axis, null);
            }
        }

        public static float EvaluateCurve(float minFactor, float maxFactor, AnimationCurve profile, float valueIn,
            float minValueIn = 0, float maxValueIn = 1)
        {
#if UNITY_EDITOR
            if (profile == null)
                return 0;
#endif
            return Mathf.Lerp(minFactor, maxFactor, profile.Evaluate(Percent(minValueIn, maxValueIn, valueIn)));
        }

        public static float AngleClosestPn(float angle, float referenceAngle)
        {
            if (referenceAngle > 0)
            {
                if (referenceAngle - angle > 180)
                    return angle + 360;
                else
                    return angle;
            }
            else if (angle - referenceAngle > 180)
                return angle - 360;
            else
                return angle;
        }

        /// <summary>
        ///     Returns the percentage equivalent of a value between 2 boundaries
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static float Percent(float from, float to, float value)
        {
            /* from=60
         * to=40
         * value =55
         * expected= 25%
         * 
         * 55-60=-5
         * 40-60=-20
         * -5/-20=25% 
         * WIN
        */
            /* if (Mathf.Approximately(from, to))
             return 1;*/
            return (value - from) / (to - from);
        }

        public static int Repeat(int value, int length)
        {
            if (value >= length)
                return length - value;
            if (value < 0)
                return length + value;

            return value;
        }

        public static float Fit(float inFloat, float minIn, float maxIn, float minOut, float maxOut)
        {
            float percent = Percent(minIn, maxIn, inFloat);
            return percent * (maxOut - minOut) + minOut;
        }

        public static float Fit(float inFloat, float minIn, float maxIn, float minOut, float maxOut,
            AnimationCurve inputCurve)
        {
            float percent = Percent(minIn, maxIn, inFloat);
            percent = inputCurve.Evaluate(percent);
            return percent * (maxOut - minOut) + minOut;
        }

        public static bool IsClampedInclusive(float value, float min, float max)
        {
            return value >= min && value <= max;
        }

        public static bool IsClampedExclusive(float value, float min, float max)
        {
            return value > min && value < max;
        }


        public static Vector3 GetLerpSpeed(Vector3 startPosition, Vector3 target, AnimationCurve speedProfile,
            float lowDistanceSpeed = 0, float highDistanceSpeed = 10, float highDistance = 10, float lowDistance = 0,
            Vector3 currentSpeed = new Vector3(), float maxAcceleration = Mathf.Infinity,
            float maxDeceleration = Mathf.Infinity)
        {
            Vector3 distance = target - startPosition;

            float delta = UnityEngine.Time.deltaTime;

#if UNITY_EDITOR

            if (!Application.isPlaying)
                delta = 0.02f;
#endif

            float wantedSpeedMag = EvaluateCurve(lowDistanceSpeed * delta,
                highDistanceSpeed * delta, speedProfile, distance.magnitude, lowDistance, highDistance);
            wantedSpeedMag = Mathf.Min(wantedSpeedMag, distance.magnitude);

            Vector3 force = distance.normalized * wantedSpeedMag - currentSpeed;

            force = force.GetClampedMagnitude(0,
                (Vector3.Dot(distance.normalized, currentSpeed.normalized) < 0)
                    ? maxDeceleration * delta
                    : maxAcceleration * delta);

            Vector3 speed = force + currentSpeed;

            return speed;
        }
        //    private static int rotationSamplesTaken = 0;

        public static void Init()
        {
            tempChild = (new GameObject("MathExt_TempChild")).transform;
            tempParent = (new GameObject("MathExt_TempParent")).transform;

            tempChild.gameObject.hideFlags = HideFlags.HideAndDontSave;
            UnityEngine.Object.DontDestroyOnLoad(tempChild.gameObject);

            tempParent.gameObject.hideFlags = HideFlags.HideAndDontSave;
            UnityEngine.Object.DontDestroyOnLoad(tempParent.gameObject);

            //set the parent
            tempChild.parent = tempParent;
        }

        public static bool IsOnLine(Vector3 vec, Vector3 origin, Vector3 direction)
        {
            return Vector3.Cross(Vector3.Project((vec - origin), direction), direction).sqrMagnitude < zeroPrecision;
        }

        //increase or decrease the length of vector by size
        public static Vector3 AddVectorLength(Vector3 vector, float size)
        {
            //get the vector length
            float magnitude = Vector3.Magnitude(vector);

            //calculate new vector length
            float newMagnitude = magnitude + size;

            //calculate the ratio of the new length to the old length
            float scale = newMagnitude / magnitude;

            //scale the vector
            return vector * scale;
        }

        //create a vector of direction "vector" with length "size"
        public static Vector3 SetVectorLength(Vector3 vector, float size)
        {
            //normalize the vector
            Vector3 vectorNormalized = Vector3.Normalize(vector);

            //scale the vector
            return vectorNormalized * size;
        }


        //caclulate the rotational difference from A to B
        public static Quaternion SubtractRotation(Quaternion b, Quaternion a)
        {
            Quaternion c = Quaternion.Inverse(a) * b;
            return c;
        }

        //Find the line of intersection between two planes.	The planes are defined by a normal and a point on that plane.
        //The outputs are a point on the line and a vector which indicates it's direction. If the planes are not parallel, 
        //the function outputs true, otherwise false.
        public static bool PlanePlaneIntersection(out Vector3 linePoint, out Vector3 lineVec, Vector3 plane1Normal,
            Vector3 plane1Position, Vector3 plane2Normal, Vector3 plane2Position)
        {
            linePoint = Vector3.zero;

            //We can get the direction of the line of intersection of the two planes by calculating the 
            //cross product of the normals of the two planes. Note that this is just a direction and the line
            //is not fixed in space yet. We need a point for that to go with the line vector.
            lineVec = Vector3.Cross(plane1Normal, plane2Normal);

            //Next is to calculate a point on the line to fix it's position in space. This is done by finding a vector from
            //the plane2 location, moving parallel to it's plane, and intersecting plane1. To prevent rounding
            //errors, this vector also has to be perpendicular to lineDirection. To get this vector, calculate
            //the cross product of the normal of plane2 and the lineDirection.		
            Vector3 ldir = Vector3.Cross(plane2Normal, lineVec);

            float denominator = Vector3.Dot(plane1Normal, ldir);

            //Prevent divide by zero and rounding errors by requiring about 5 degrees angle between the planes.
            if (Mathf.Abs(denominator) > 0.006f)
            {
                Vector3 plane1ToPlane2 = plane1Position - plane2Position;
                float t = Vector3.Dot(plane1Normal, plane1ToPlane2) / denominator;
                linePoint = plane2Position + t * ldir;

                return true;
            }

            //output not valid
            else
            {
                return false;
            }
        }

        //Get the intersection between a line and a plane. 
        //If the line and plane are not parallel, the function outputs true, otherwise false.
        public static bool LinePlaneIntersection(out Vector3 intersection, Vector3 linePoint, Vector3 lineVec,
            Vector3 planeNormal, Vector3 planePoint)
        {
            intersection = Vector3.zero;

            //calculate the distance between the linePoint and the line-plane intersection point
            float dotNumerator = Vector3.Dot((planePoint - linePoint), planeNormal);
            float dotDenominator = Vector3.Dot(lineVec, planeNormal);

            //line and plane are not parallel
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (dotDenominator != 0.0f)
            {
                float length = dotNumerator / dotDenominator;

                //create a vector from the linePoint to the intersection point
                Vector3 vector = SetVectorLength(lineVec, length);

                //get the coordinates of the line-plane intersection point
                intersection = linePoint + vector;

                return true;
            }

            //output not valid

            return false;
        }

        //Calculate the intersection point of two lines. Returns true if lines intersect, otherwise false.
        //Note that in 3d, two lines do not intersect most of the time. So if the two lines are not in the 
        //same plane, use ClosestPointsOnTwoLines() instead.
        public static bool LineLineIntersection(out Vector3 intersection, Vector3 linePoint1, Vector3 lineVec1,
            Vector3 linePoint2, Vector3 lineVec2)
        {
            intersection = Vector3.zero;

            Vector3 lineVec3 = linePoint2 - linePoint1;
            Vector3 crossVec1And2 = Vector3.Cross(lineVec1, lineVec2);
            Vector3 crossVec3And2 = Vector3.Cross(lineVec3, lineVec2);

            float planarFactor = Vector3.Dot(lineVec3, crossVec1And2);

            //Lines are not coplanar. Take into account rounding errors.
            if ((planarFactor >= 0.00001f) || (planarFactor <= -0.00001f))
            {
                return false;
            }

            //Note: sqrMagnitude does x*x+y*y+z*z on the input vector.
            float s = Vector3.Dot(crossVec3And2, crossVec1And2) / crossVec1And2.sqrMagnitude;

            if ((s >= 0.0f) && (s <= 1.0f))
            {
                intersection = linePoint1 + (lineVec1 * s);
                return true;
            }

            else
            {
                return false;
            }
        }

        //Two non-parallel lines which may or may not touch each other have a point on each line which are closest
        //to each other. This function finds those two points. If the lines are not parallel, the function 
        //outputs true, otherwise false.
        public static bool ClosestPointsOnTwoLines(out Vector3 closestPointLine1, out Vector3 closestPointLine2,
            Vector3 linePoint1, Vector3 lineVec1, Vector3 linePoint2, Vector3 lineVec2)
        {
            closestPointLine1 = Vector3.zero;
            closestPointLine2 = Vector3.zero;

            float a = Vector3.Dot(lineVec1, lineVec1);
            float b = Vector3.Dot(lineVec1, lineVec2);
            float e = Vector3.Dot(lineVec2, lineVec2);

            float d = a * e - b * b;

            //lines are not parallel
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (d != 0.0f)
            {
                Vector3 r = linePoint1 - linePoint2;
                float c = Vector3.Dot(lineVec1, r);
                float f = Vector3.Dot(lineVec2, r);

                float s = (b * f - c * e) / d;
                float t = (a * f - c * b) / d;

                closestPointLine1 = linePoint1 + lineVec1 * s;
                closestPointLine2 = linePoint2 + lineVec2 * t;

                return true;
            }

            else
            {
                return false;
            }
        }

        //This function returns a point which is a projection from a point to a line.
        //The line is regarded infinite. If the line is finite, use ProjectPointOnLineSegment() instead.
        public static Vector3 ProjectPointOnLine(Vector3 linePoint, Vector3 lineVec, Vector3 point)
        {
            //get vector from point on line to point in space
            Vector3 linePointToPoint = point - linePoint;

            float t = Vector3.Dot(linePointToPoint, lineVec);

            return linePoint + lineVec * t;
        }

        //This function returns a point which is a projection from a point to a line segment.
        //If the projected point lies outside of the line segment, the projected point will 
        //be clamped to the appropriate line edge.
        //If the line is infinite instead of a segment, use ProjectPointOnLine() instead.
        public static Vector3 ProjectPointOnLineSegment(Vector3 linePoint1, Vector3 linePoint2, Vector3 point)
        {
            Vector3 vector = linePoint2 - linePoint1;

            Vector3 projectedPoint = ProjectPointOnLine(linePoint1, vector.normalized, point);

            int side = PointOnWhichSideOfLineSegment(linePoint1, linePoint2, projectedPoint);

            //The projected point is on the line segment
            if (side == 0)
            {
                return projectedPoint;
            }

            if (side == 1)
            {
                return linePoint1;
            }

            if (side == 2)
            {
                return linePoint2;
            }

            //output is invalid
            return Vector3.zero;
        }

        //This function returns a point which is a projection from a point to a plane.
        public static Vector3 ProjectPointOnPlane(Vector3 planeNormal, Vector3 planePoint, Vector3 point)
        {
            //First calculate the distance from the point to the plane:
            float distance = SignedDistancePlanePoint(planeNormal, planePoint, point);

            //Reverse the sign of the distance
            distance *= -1;

            //Get a translation vector
            Vector3 translationVector = SetVectorLength(planeNormal, distance);

            //Translate the point to form a projection
            return point + translationVector;
        }

        //Projects a vector onto a plane. The output is not normalized.
        public static Vector3 ProjectVectorOnPlane(Vector3 vector, Vector3 planeNormal)
        {
            return vector - (Vector3.Dot(vector, planeNormal) * planeNormal);
        }

        //Get the shortest distance between a point and a plane. The output is signed so it holds information
        //as to which side of the plane normal the point is.
        public static float SignedDistancePlanePoint(Vector3 planeNormal, Vector3 planePoint, Vector3 point)
        {
            return Vector3.Dot(planeNormal, (point - planePoint));
        }

        //This function calculates a signed (+ or - sign instead of being ambiguous) dot product. It is basically used
        //to figure out whether a vector is positioned to the left or right of another vector. The way this is done is
        //by calculating a vector perpendicular to one of the vectors and using that as a reference. This is because
        //the result of a dot product only has signed information when an angle is transitioning between more or less
        //then 90 degrees.
        public static float SignedDotProduct(Vector3 vectorA, Vector3 vectorB, Vector3 normal)
        {
            //Use the geometry object normal and one of the input vectors to calculate the perpendicular vector
            Vector3 perpVector = Vector3.Cross(normal, vectorA);

            //Now calculate the dot product between the perpendicular vector (perpVector) and the other input vector
            float dot = Vector3.Dot(perpVector, vectorB);

            return dot;
        }

        public static float SignedVectorAngle(Vector3 referenceVector, Vector3 otherVector, Vector3 normal)
        {
            //Use the geometry object normal and one of the input vectors to calculate the perpendicular vector
            Vector3 perpVector = Vector3.Cross(normal, referenceVector);

            //Now calculate the dot product between the perpendicular vector (perpVector) and the other input vector
            float angle = Vector3.Angle(referenceVector, otherVector);
            angle *= Mathf.Sign(Vector3.Dot(perpVector, otherVector));

            return angle;
        }

        //Calculate the angle between a vector and a plane. The plane is made by a normal vector.
        //Output is in radians.
        public static float AngleVectorPlaneRad(Vector3 vector, Vector3 normal)
        {
            //calculate the the dot product between the two input vectors. This gives the cosine between the two vectors
            float dot = Vector3.Dot(vector, normal);

            //this is in radians
            float angle = (float) Math.Acos(dot);

            return 1.570796326794897f - angle; //90 degrees - angle
        }

        public static float AngleVectorPlaneDeg(Vector3 vector, Vector3 normal, Vector3 planeOrtho)
        {
            //calculate the the dot product between the two input vectors. This gives the cosine between the two vectors
            float dot = Vector3.Dot(normal, vector);

            //this is in radians
            float angle = Mathf.Rad2Deg * Mathf.Acos(dot);

            return 90 - angle; //90 degrees - angle
        }

        //Calculate the dot product as an angle
        public static float DotProductAngle(Vector3 vec1, Vector3 vec2)
        {
            //get the dot product
            double dot = Vector3.Dot(vec1, vec2);

            //Clamp to prevent NaN error. Shouldn't need this in the first place, but there could be a rounding error issue.
            if (dot < -1.0f)
            {
                dot = -1.0f;
            }

            if (dot > 1.0f)
            {
                dot = 1.0f;
            }

            //Calculate the angle. The output is in radians
            //This step can be skipped for optimization...
            double angle = Math.Acos(dot);

            return (float) angle;
        }

        //Convert a plane defined by 3 points to a plane defined by a vector and a point. 
        //The plane point is the middle of the triangle defined by the 3 points.
        public static void PlaneFrom3Points(out Vector3 planeNormal, out Vector3 planePoint, Vector3 pointA, Vector3 pointB,
            Vector3 pointC)
        {
            //Make two vectors from the 3 input points, originating from point A
            Vector3 ab = pointB - pointA;
            Vector3 ac = pointC - pointA;

            //Calculate the normal
            planeNormal = Vector3.Normalize(Vector3.Cross(ab, ac));

            //Get the points in the middle AB and AC
            Vector3 middleAb = pointA + (ab / 2.0f);
            Vector3 middleAc = pointA + (ac / 2.0f);

            //Get vectors from the middle of AB and AC to the point which is not on that line.
            Vector3 middleABtoC = pointC - middleAb;
            Vector3 middleACtoB = pointB - middleAc;

            //Calculate the intersection between the two lines. This will be the center 
            //of the triangle defined by the 3 points.
            //We could use LineLineIntersection instead of ClosestPointsOnTwoLines but due to rounding errors 
            //this sometimes doesn't work.
            Vector3 temp;
            ClosestPointsOnTwoLines(out planePoint, out temp, middleAb, middleABtoC, middleAc, middleACtoB);
        }

        //Returns the forward vector of a quaternion
        public static Vector3 GetForwardVector(Quaternion q)
        {
            return q * Vector3.forward;
        }

        //Returns the up vector of a quaternion
        public static Vector3 GetUpVector(Quaternion q)
        {
            return q * Vector3.up;
        }

        //Returns the right vector of a quaternion
        public static Vector3 GetRightVector(Quaternion q)
        {
            return q * Vector3.right;
        }

        //Gets a quaternion from a matrix
        public static Quaternion QuaternionFromMatrix(Matrix4x4 m)
        {
            return Quaternion.LookRotation(m.GetColumn(2), m.GetColumn(1));
        }

        //Gets a position from a matrix
        public static Vector3 PositionFromMatrix(Matrix4x4 m)
        {
            Vector4 vector4Position = m.GetColumn(3);
            return new Vector3(vector4Position.x, vector4Position.y, vector4Position.z);
        }

        //This is an alternative for Quaternion.LookRotation. Instead of aligning the forward and up vector of the game 
        //object with the input vectors, a custom direction can be used instead of the fixed forward and up vectors.
        //alignWithVector and alignWithNormal are in world space.
        //customForward and customUp are in object space.
        //Usage: use alignWithVector and alignWithNormal as if you are using the default LookRotation function.
        //Set customForward and customUp to the vectors you wish to use instead of the default forward and up vectors.
        public static void LookRotationExtended(ref GameObject gameObjectInOut, Vector3 alignWithVector,
            Vector3 alignWithNormal, Vector3 customForward, Vector3 customUp)
        {
            //Set the rotation of the destination
            Quaternion rotationA = Quaternion.LookRotation(alignWithVector, alignWithNormal);

            //Set the rotation of the custom normal and up vectors. 
            //When using the default LookRotation function, this would be hard coded to the forward and up vector.
            Quaternion rotationB = Quaternion.LookRotation(customForward, customUp);

            //Calculate the rotation
            gameObjectInOut.transform.rotation = rotationA * Quaternion.Inverse(rotationB);
        }

        //This function transforms one object as if it was parented to the other.
        //Before using this function, the Init() function must be called
        //Input: parentRotation and parentPosition: the current parent transform.
        //Input: startParentRotation and startParentPosition: the transform of the parent object at the time the objects are parented.
        //Input: startChildRotation and startChildPosition: the transform of the child object at the time the objects are parented.
        //Output: childRotation and childPosition.
        //All transforms are in world space.
        public static void TransformWithParent(out Quaternion childRotation, out Vector3 childPosition,
            Quaternion parentRotation, Vector3 parentPosition, Quaternion startParentRotation, Vector3 startParentPosition,
            Quaternion startChildRotation, Vector3 startChildPosition)
        {
            //set the parent start transform
            tempParent.rotation = startParentRotation;
            tempParent.position = startParentPosition;
            tempParent.localScale = Vector3.one; //to prevent scale wandering

            //set the child start transform
            tempChild.rotation = startChildRotation;
            tempChild.position = startChildPosition;
            tempChild.localScale = Vector3.one; //to prevent scale wandering

            //translate and rotate the child by moving the parent
            tempParent.rotation = parentRotation;
            tempParent.position = parentPosition;

            //get the child transform
            childRotation = tempChild.rotation;
            childPosition = tempChild.position;
        }

        //With this function you can align a triangle of an object with any transform.
        //Usage: gameObjectInOut is the game object you want to transform.
        //alignWithVector, alignWithNormal, and alignWithPosition is the transform with which the triangle of the object should be aligned with.
        //triangleForward, triangleNormal, and trianglePosition is the transform of the triangle from the object.
        //alignWithVector, alignWithNormal, and alignWithPosition are in world space.
        //triangleForward, triangleNormal, and trianglePosition are in object space.
        //trianglePosition is the mesh position of the triangle. The effect of the scale of the object is handled automatically.
        //trianglePosition can be set at any position, it does not have to be at a vertex or in the middle of the triangle.
        public static void PreciseAlign(ref GameObject gameObjectInOut, Vector3 alignWithVector, Vector3 alignWithNormal,
            Vector3 alignWithPosition, Vector3 triangleForward, Vector3 triangleNormal, Vector3 trianglePosition)
        {
            //Set the rotation.
            LookRotationExtended(ref gameObjectInOut, alignWithVector, alignWithNormal, triangleForward, triangleNormal);

            //Get the world space position of trianglePosition
            Vector3 trianglePositionWorld = gameObjectInOut.transform.TransformPoint(trianglePosition);

            //Get a vector from trianglePosition to alignWithPosition
            Vector3 translateVector = alignWithPosition - trianglePositionWorld;

            //Now transform the object so the triangle lines up correctly.
            gameObjectInOut.transform.Translate(translateVector, Space.World);
        }


        //Convert a position, direction, and normal vector to a transform
        public static void VectorsToTransform(ref GameObject gameObjectInOut, Vector3 positionVector,
            Vector3 directionVector, Vector3 normalVector)
        {
            gameObjectInOut.transform.position = positionVector;
            gameObjectInOut.transform.rotation = Quaternion.LookRotation(directionVector, normalVector);
        }

        //This function finds out on which side of a line segment the point is located.
        //The point is assumed to be on a line created by linePoint1 and linePoint2. If the point is not on
        //the line segment, project it on the line using ProjectPointOnLine() first.
        //Returns 0 if point is on the line segment.
        //Returns 1 if point is outside of the line segment and located on the side of linePoint1.
        //Returns 2 if point is outside of the line segment and located on the side of linePoint2.
        public static int PointOnWhichSideOfLineSegment(Vector3 linePoint1, Vector3 linePoint2, Vector3 point)
        {
            Vector3 lineVec = linePoint2 - linePoint1;
            Vector3 pointVec = point - linePoint1;

            float dot = Vector3.Dot(pointVec, lineVec);

            //point is on side of linePoint2, compared to linePoint1
            if (dot > 0)
            {
                //point is on the line segment
                if (pointVec.magnitude <= lineVec.magnitude)
                {
                    return 0;
                }

                //point is not on the line segment and it is on the side of linePoint2
                else
                {
                    return 2;
                }
            }

            //Point is not on side of linePoint2, compared to linePoint1.
            //Point is not on the line segment and it is on the side of linePoint1.
            else
            {
                return 1;
            }
        }


        //Returns the pixel distance from the mouse pointer to a line.
        //Alternative for HandleUtility.DistanceToLine(). Works both in Editor mode and Play mode.
        //Do not call this function from OnGUI() as the mouse position will be wrong.
        public static float MouseDistanceToLine(Vector3 linePoint1, Vector3 linePoint2)
        {
            Camera currentCamera = Camera.main;
            Vector3 mousePosition;

#if MOBILE_INPUT
        mousePosition = Input.touches[0].position ;
#else
            if (Camera.current != null)
            {
                currentCamera = Camera.current;
            }

            else
            {
                currentCamera = Camera.main;
            }

            //convert format because y is flipped
            mousePosition = new Vector3(Event.current.mousePosition.x,
                currentCamera.pixelHeight - Event.current.mousePosition.y, 0f);


            mousePosition = UnityEngine.Input.mousePosition;

#endif
            Vector3 screenPos1 = currentCamera.WorldToScreenPoint(linePoint1);
            Vector3 screenPos2 = currentCamera.WorldToScreenPoint(linePoint2);
            Vector3 projectedPoint = ProjectPointOnLineSegment(screenPos1, screenPos2, mousePosition);

            //set z to zero
            projectedPoint = new Vector3(projectedPoint.x, projectedPoint.y, 0f);

            Vector3 vector = projectedPoint - mousePosition;
            return vector.magnitude;
        }


        //Returns the pixel distance from the mouse pointer to a camera facing circle.
        //Alternative for HandleUtility.DistanceToCircle(). Works both in Editor mode and Play mode.
        //Do not call this function from OnGUI() as the mouse position will be wrong.
        //If you want the distance to a point instead of a circle, set the debugRadius to 0.
        public static float MouseDistanceToCircle(Vector3 point, float radius)
        {
            Camera currentCamera = Camera.main;
            Vector3 mousePosition = Vector3.zero;
#if UNITY_EDITOR

#elif MOBILE_INPUT
        mousePosition = Input.touches[0].position;
#else
        if (Camera.current != null)
        {

            currentCamera = Camera.current;
        }

        else
        {

            currentCamera = Camera.main;
        }

        //convert format because y is flipped
        mousePosition =
 new Vector3(Event.current.mousePosition.x, currentCamera.pixelHeight - Event.current.mousePosition.y, 0f);

        mousePosition = Input.mousePosition;
#endif

            Vector3 screenPos = currentCamera.WorldToScreenPoint(point);

            //set z to zero
            screenPos = new Vector3(screenPos.x, screenPos.y, 0f);

            Vector3 vector = screenPos - mousePosition;
            float fullDistance = vector.magnitude;
            float circleDistance = fullDistance - radius;

            return circleDistance;
        }

        //Returns true if a line segment (made up of linePoint1 and linePoint2) is fully or partially in a rectangle
        //made up of RectA to RectD. The line segment is assumed to be on the same plane as the rectangle. If the line is 
        //not on the plane, use ProjectPointOnPlane() on linePoint1 and linePoint2 first.
        public static bool IsLineInRectangle(Vector3 linePoint1, Vector3 linePoint2, Vector3 rectA, Vector3 rectB,
            Vector3 rectC, Vector3 rectD)
        {
            bool pointBInside = false;

            bool pointAInside = IsPointInRectangle(linePoint1, rectA, rectC, rectB, rectD);

            if (!pointAInside)
            {
                pointBInside = IsPointInRectangle(linePoint2, rectA, rectC, rectB, rectD);
            }

            //none of the points are inside, so check if a line is crossing
            if (!pointAInside && !pointBInside)
            {
                bool lineACrossing = AreLineSegmentsCrossing(linePoint1, linePoint2, rectA, rectB);
                bool lineBCrossing = AreLineSegmentsCrossing(linePoint1, linePoint2, rectB, rectC);
                bool lineCCrossing = AreLineSegmentsCrossing(linePoint1, linePoint2, rectC, rectD);
                bool lineDCrossing = AreLineSegmentsCrossing(linePoint1, linePoint2, rectD, rectA);

                if (lineACrossing || lineBCrossing || lineCCrossing || lineDCrossing)
                {
                    return true;
                }

                else
                {
                    return false;
                }
            }

            else
            {
                return true;
            }
        }

        //Returns true if "point" is in a rectangle made up of RectA to RectD. The line point is assumed to be on the same 
        //plane as the rectangle. If the point is not on the plane, use ProjectPointOnPlane() first.
        public static bool IsPointInRectangle(Vector3 point, Vector3 rectA, Vector3 rectC, Vector3 rectB, Vector3 rectD)
        {
            //get the center of the rectangle
            Vector3 vector = rectC - rectA;
            float size = -(vector.magnitude / 2f);
            vector = AddVectorLength(vector, size);
            Vector3 middle = rectA + vector;

            Vector3 xVector = rectB - rectA;
            float width = xVector.magnitude / 2f;

            Vector3 yVector = rectD - rectA;
            float height = yVector.magnitude / 2f;

            Vector3 linePoint = ProjectPointOnLine(middle, xVector.normalized, point);
            vector = linePoint - point;
            float yDistance = vector.magnitude;

            linePoint = ProjectPointOnLine(middle, yVector.normalized, point);
            vector = linePoint - point;
            float xDistance = vector.magnitude;

            if ((xDistance <= width) && (yDistance <= height))
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        /// <summary>
        ///     FRONT IS WHAT IS FREAKING FURTHER MAN : bigger z!!!!!
        /// </summary>
        /// <param name="point"></param>
        /// <param name="topFrontRight"></param>
        /// <param name="bottomBackLeft"></param>
        /// <returns></returns>
        public static bool IsPointInCube(Vector3 point, Vector3 topFrontRight, Vector3 bottomBackLeft)
        {
            return ((point.x <= topFrontRight.x && point.x >= bottomBackLeft.x)
                    && (point.y >= bottomBackLeft.y && point.y <= topFrontRight.y)
                    && (point.z <= topFrontRight.z && point.z >= bottomBackLeft.z));
        }

        //Returns true if line segment made up of pointA1 and pointA2 is crossing line segment made up of
        //pointB1 and pointB2. The two lines are assumed to be in the same plane.
        public static bool AreLineSegmentsCrossing(Vector3 pointA1, Vector3 pointA2, Vector3 pointB1, Vector3 pointB2)
        {
            Vector3 closestPointA;
            Vector3 closestPointB;
            int sideA;
            int sideB;

            Vector3 lineVecA = pointA2 - pointA1;
            Vector3 lineVecB = pointB2 - pointB1;

            bool valid = ClosestPointsOnTwoLines(out closestPointA, out closestPointB, pointA1, lineVecA.normalized,
                pointB1, lineVecB.normalized);

            //lines are not parallel
            if (valid)
            {
                sideA = PointOnWhichSideOfLineSegment(pointA1, pointA2, closestPointA);
                sideB = PointOnWhichSideOfLineSegment(pointB1, pointB2, closestPointB);

                if ((sideA == 0) && (sideB == 0))
                {
                    return true;
                }

                else
                {
                    return false;
                }
            }

            //lines are parallel
            else
            {
                return false;
            }
        }

        //This function calculates the acceleration vector in meter/second^2.
        //Input: position. If the output is used for motion simulation, the input transform
        //has to be located at the seat base, not at the vehicle CG. NotifyLinkedSceneLoaded an empty GameObject
        //at the correct location and use that as the input for this function.
        //Gravity is not taken into account but this can be added to the output if needed.
        //A low number of samples can give a jittery result due to rounding errors.
        //If more samples are used, the output is more smooth but has a higher latency.
        public static bool LinearAcceleration(out Vector3 vector, Vector3 position, int samples)
        {
            Vector3 averageSpeedChange = Vector3.zero;
            vector = Vector3.zero;
            Vector3 deltaDistance;
            float deltaTime;
            Vector3 speedA;
            Vector3 speedB;

            //Clamp sample amount. In order to calculate acceleration we need at least 2 changes
            //in duration, so we need at least 3 position samples.
            if (samples < 3)
            {
                samples = 3;
            }

            //Initialize
            if (positionRegister == null)
            {
                positionRegister = new Vector3[samples];
                posTimeRegister = new float[samples];
            }

            //Fill the position and time sample array and shift the location in the array to the left
            //each time a new sample is taken. This way index 0 will always hold the oldest sample and the
            //highest index will always hold the newest sample. 
            for (int i = 0; i < positionRegister.Length - 1; i++)
            {
                positionRegister[i] = positionRegister[i + 1];
                posTimeRegister[i] = posTimeRegister[i + 1];
            }

            positionRegister[positionRegister.Length - 1] = position;
            posTimeRegister[posTimeRegister.Length - 1] = UnityEngine.Time.time;

            positionSamplesTaken++;

            //The output acceleration can only be calculated if enough samples are taken.
            if (positionSamplesTaken >= samples)
            {
                //Calculate average duration change.
                for (int i = 0; i < positionRegister.Length - 2; i++)
                {
                    deltaDistance = positionRegister[i + 1] - positionRegister[i];
                    deltaTime = posTimeRegister[i + 1] - posTimeRegister[i];

                    //If deltaTime is 0, the output is invalid.
                    if (deltaTime <= zeroPrecision)
                    {
                        return false;
                    }

                    speedA = deltaDistance / deltaTime;
                    deltaDistance = positionRegister[i + 2] - positionRegister[i + 1];
                    deltaTime = posTimeRegister[i + 2] - posTimeRegister[i + 1];

                    if (deltaTime <= zeroPrecision)
                    {
                        return false;
                    }

                    speedB = deltaDistance / deltaTime;

                    //This is the accumulated duration change at this stage, not the average yet.
                    averageSpeedChange += speedB - speedA;
                }

                //Now this is the average duration change.
                averageSpeedChange /= positionRegister.Length - 2;

                //Get the total time difference.
                float deltaTimeTotal = posTimeRegister[posTimeRegister.Length - 1] - posTimeRegister[0];

                //Now calculate the acceleration, which is an average over the amount of samples taken.
                vector = averageSpeedChange / deltaTimeTotal;

                return true;
            }

            return false;
        }

        //Get y from a linear function, with x as an input. The linear function goes through points
        //Pxy on the left ,and Qxy on the right.
        public static float LinearFunction2DFull(float x, float px, float py, float qx, float qy)
        {
            float a = qy - py;
            float b = qx - px;
            float c = a / b;

            float y = py + (c * (x - px));

            return y;
        }

        public static Vector3 Center(params Vector3[] positions)
        {
            return GravityCenter(positions);
        }

        public static Vector3 GravityCenter(IEnumerable<Vector3> positions)
        {
            Vector3 ret = new Vector3();
            float div = 0;
            foreach (Vector3 vec3 in positions)
            {
                ret += (Vector3) vec3;
                div++;
            }

            ret /= (div <= 0) ? 1 : div;
            return ret;
        }

        public static Vector3 Barycenter(params Vector4[] vecAndParam)
        {
            Vector3 ret = new Vector3();
            float div = 0;
            foreach (Vector4 vec4 in vecAndParam)
            {
                ret += (Vector3) vec4 * vec4.w;
                div += vec4.w;
            }

            ret /= (div <= 0) ? 1 : div;
            return ret;
        }

        public static Vector2 Barycenter(params Vector3[] vecAndParam)
        {
            Vector2 ret = new Vector2();
            float div = 0;
            foreach (Vector3 vec4 in vecAndParam)
            {
                ret += (Vector2) vec4 * vec4.z;
                div += vec4.z;
            }

            ret /= (div <= 0) ? 1 : div;
            return ret;
        }

        public static float Barycenter(params Vector2[] vecAndParam)
        {
            float ret = 0;
            float div = 0;
            foreach (Vector2 vec4 in vecAndParam)
            {
                UnityEngine.Debug.Log(vec4);
                ret += vec4.x * vec4.y;
                div += vec4.y;
            }

            ret /= (div <= 0) ? 1 : div;
            return ret;
        }
    }

    public static class RandomExt
    {
        public static Vector3 RandomVectorBetween(Vector3 first, Vector3 second)
        {
            return Vector3.Lerp(first, second, UnityEngine.Random.Range(0f, 1f));
        }

        /// <summary>
        ///     The Vectors here are assumed to be coplanar, if they are not that's your problem!
        /// </summary>
        /// <param name="topRight"></param>
        /// <param name="topLeft"></param>
        /// <param name="bottomRight"></param>
        /// <param name="bottomLeft"></param>
        /// <returns></returns>
        public static Vector3 RandomVectorInQuad(Vector3 topRight, Vector3 topLeft, Vector3 bottomRight,
            Vector3 bottomLeft)
        {
            Vector3 topRand = RandomVectorBetween(topRight, topLeft);
            Vector3 bottomRand = RandomVectorBetween(bottomRight, bottomLeft);
            return RandomVectorBetween(topRand, bottomRand);
        }

        public static Vector3 RandomSphereSurfacePoint(Vector3 center, float radius)
        {
            var u = UnityEngine.Random.value;
            var v = UnityEngine.Random.value;
            float theta = 2 * Mathf.PI * u;
            var phi = Mathf.Acos(2 * v - 1);
            var x = center.x + (radius * Mathf.Sin(phi) * Mathf.Cos(theta));
            var y = center.y + (radius * Mathf.Sin(phi) * Mathf.Sin(theta));
            var z = center.z + (radius * Mathf.Cos(phi));
            return new Vector3(x, y, z);
        }
    }
}
