using System;
using System.Text;
using UnityEngine;

namespace BFT
{
    public static class TimeUtils
    {
        // ReSharper disable once InconsistentNaming not respected to fit Unity and make it easier to refactor
        public static float safeDeltaTime
        {
            get
            {
#if UNITY_EDITOR
                if (Application.isPlaying)
                {
                    return Mathf.Max(UnityEngine.Time.deltaTime, 0.000001f);
                }

                //simulate a steady 60 FPS
                //return 0.016f;
                //return a full second
                return 1;
#else
            return Time.deltaTime;
#endif
            }
        }

        public static string FormattedString(float duration, float maxDurationToShowMiliSeconds = 5,
            float maxDurationToShowSeconds = 3600, float maxDurationToShowMinutes = 82800,
            float minDurationToShowMinutes = 60, float maxDurationToShowHours = 172800,
            float minDurationToShowHours = 3600, float minDurationToShowDays = 86400, string dayNotice = "d",
            string hourNotice = "h", string minuteNotice = "m", string secondNotice = "s", string millisecondNotice = "ms")
        {
            TimeSpan t = TimeSpan.FromSeconds(duration);

            StringBuilder builder = new StringBuilder();

            bool showDay = false;
            bool showHours = false;
            bool showMinutes = false;
            bool showSeconds = false;

            if (duration > minDurationToShowDays)
            {
                //D3, D2 etc to show zero before value, could become a parameter
                builder.Append("{0}");
                builder.Append(dayNotice);
                showDay = true;
            }

            if (duration > minDurationToShowHours && duration <= maxDurationToShowHours)
            {
                if (showDay)
                {
                    builder.Append(",");
                    builder.Append("{1:D2}");
                }
                else
                {
                    builder.Append("{1}");
                }

                builder.Append(hourNotice);
                showHours = true;
            }

            if (duration > minDurationToShowMinutes && duration <= maxDurationToShowMinutes)
            {
                if (showHours)
                {
                    builder.Append(":");
                    builder.Append("{2:D2}");
                }
                else
                    builder.Append("{2}");

                builder.Append(minuteNotice);
                showMinutes = true;
            }


            if (duration <= maxDurationToShowSeconds)
            {
                if (showMinutes)
                {
                    builder.Append(":");
                    builder.Append("{3:D2}");
                }
                else
                {
                    builder.Append("{3}");
                }

                builder.Append(secondNotice);

                showSeconds = true;
            }

            if (duration <= maxDurationToShowMiliSeconds)
            {
                if (showSeconds)
                {
                    builder.Append(":");
                    builder.Append("{4:D3}");
                }
                else
                {
                    builder.Append("{4}");
                }

                builder.Append(millisecondNotice);
            }

            return string.Format(builder.ToString(),
                t.Days,
                t.Hours,
                t.Minutes,
                t.Seconds,
                t.Milliseconds);
        }
    }
}
