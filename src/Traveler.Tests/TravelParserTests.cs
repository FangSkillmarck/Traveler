using Shouldly;
using System;
using Xunit;

namespace Traveler.Tests
{
    public class TravelParserTests
    {
        [Fact]
        public void Should_Run_Single_Scenario_With_Expected_Outcome()
        {
            // Given
            var input = "POS=0,0,E\r\nFFFRFFF";

            // When
            var result = TravelParser.Run(input);

            // Then
           

            result[0].Item1.ShouldBe(3);
            result[0].Item2.ShouldBe(3);
            result[0].Item3.ShouldBe('S'); 
            result.Length.ShouldBe(1);
        }

        [Fact]
        public void Should_Run_Multiple_Scenarios_With_Expected_Outcome()
        {
            // Given
            var input = "POS=0,0,E\r\nFFFRFFF\r\nPOS=6,4,W\r\nBLFLFFFFFRFRFLFL\r\nBLBLBRBL\r\nPOS=1,1,N";

            // When
            var result = TravelParser.Run(input);

            // Then
            result.Length.ShouldBe(3);

            result[0].Item1.ShouldBe(3);
            result[0].Item2.ShouldBe(3);
            result[0].Item3.ShouldBe('S');

            result[1].Item1.ShouldBe(11);
            result[1].Item2.ShouldBe(9);
            result[1].Item3.ShouldBe('W');

            result[2].Item1.ShouldBe(1);
            result[2].Item2.ShouldBe(1);
            result[2].Item3.ShouldBe('N');
        }

        [Fact]
        public void Should_Not_Require_Operations()
        {
            // Given
            var input = "POS=1,1,N";

            // When
            var result = TravelParser.Run(input);

            // Then
            result.Length.ShouldBe(1);

            result[0].Item1.ShouldBe(1);
            result[0].Item2.ShouldBe(1);
            result[0].Item3.ShouldBe('N');
        }

        [Fact]
        public void Should_Support_Single_Line_Feed_As_Separator()
        {
            // Given
            var input = "POS=0,0,E\nFFFRFFF";

            // When
            var result = TravelParser.Run(input);

            // Then
            result.Length.ShouldBe(1);

            result[0].Item1.ShouldBe(3);
            result[0].Item2.ShouldBe(3);
            result[0].Item3.ShouldBe('S');
        }

        [Fact]
        public void Should_Support_Comments()
        {
            // Given
            var input = "//Hello World\r\nPOS=0,0,E\r\nFFFRFFF";

            // When
            var result = TravelParser.Run(input);

            // Then
            result.Length.ShouldBe(1);

            result[0].Item1.ShouldBe(3);
            result[0].Item2.ShouldBe(3);
            result[0].Item3.ShouldBe('S');
        }
    }
}
