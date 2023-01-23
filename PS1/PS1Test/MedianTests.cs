using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace PS1Test
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMedian1()
		{
			int[] array = { 1, 2, 3, 4, 5 };
			int[] array2 = { 1, 1, 1, 1, 1 };
			/*
			 * 5
			1 2 3 4 5
			1 1 1 1 1
			 */
			//Assert.AreEqual(3, PS1.PS1.quickselect(array, array2, ((array.Length-1)/2)));
			Assert.AreEqual(3, PS1.PS1.Momselect(array, array2, ((array.Length-1)/2)));
		}

		/*[TestMethod]
		public void TestWeight1()
		{
			Dictionary<int, int> dict = new Dictionary<int, int>();
			int[] array = { 1, 2, 3, 4, 5 };
			int[] array2 = { 1, 1, 1, 1, 1 };
			for(int i = 1; i < array.Length;i++)
			{
				dict.Add(array[i], array2[i]);
			}

			 * 5
			1 2 3 4 5
			1 1 1 1 1

			Assert.AreEqual(3, PS1.PS1.WeightSelect(dict,array, array2, (array.Length/2)));
		}*/

		[TestMethod]
		public void TestMedian2()
		{
			int[] array = { 2, 1, 4, 5, 3 };
			int[] array2 = { 1, 1, 1, 1, 1 };
			/*
			5
			2 1 4 5 3
			1 1 1 1 1
			 */
			//Assert.AreEqual(3, PS1.PS1.quickselect(array, array2, (array.Length-1)/2));
			Assert.AreEqual(3, PS1.PS1.Momselect(array, array2, (array.Length-1)/2));
		}

		[TestMethod]
		public void TestWeightedMedian1()
		{
			int[] array = { 2, 1, 4, 5, 3 };
			int[] array2 = { 8, 6, 4, 2, 1 };
			/*
			5
			2 1 4 5 3
			1 1 1 1 1
			 */
			Assert.AreEqual(2, PS1.PS1.Momselect(array, array2, (array.Length-1)/2));
		}

		[TestMethod]
		public void TestWeightedMedian2()
		{
			int[] array = { 1, 2, 4, 5 };
			int[] array2 = { 1, 5, 5, 1 };
			/*
			5
			2 1 4 5 3
			1 1 1 1 1
			 */
			//Assert.AreEqual(4, PS1.PS1.quickselect(array, array2, (array.Length-1)/2));
			int val = PS1.PS1.Momselect(array, array2, (array.Length)/2);
			Assert.IsTrue(2 == val || 4 == val);
		}

		[TestMethod]
		public void TestBigMedian()
		{
			int[] array = new int[100];
			int[] array2 = new int[100];
			for (int i = 1; i < 101; i++)
			{
				array[i-1] = i;
				array2[i-1] = 1;
			}
			/*
			5
			2 1 4 5 3
			1 1 1 1 1
			 */
			//Assert.AreEqual(array.Length/2, PS1.PS1.quickselect(array, array2, (array.Length)/2));
			Assert.AreEqual(array.Length /2, PS1.PS1.Momselect(array, array2, (array.Length - 1)/2));
		}
		[TestMethod]
		public void TestBiggliestMedian()
		{
			int[] array = new int[1000000];
			int[] array2 = new int[1000000];
			for (int i = 1; i < 1000001; i++)
			{
				array[i -1] = i;
				array2[i -1] = 1;
			}
			//Assert.AreEqual(array.Length/2, PS1.PS1.quickselect(array, array2, (array.Length-1)/2));
			Assert.AreEqual(array.Length/2, PS1.PS1.Momselect(array, array2, (array.Length - 1)/2));
		}
		[TestMethod]
		public void TestMedOfMed()
		{
			int[] array = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25 };
			int[] array2 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25 };
			int[] array3 = { 3, 8, 13, 18, 23 };
			//Assert.AreEqual(13, PS1.PS1.quickselect(array, array2, (array.Length-1)/2));
			Assert.AreEqual(13, PS1.PS1.Momselect(array, array2, (array.Length-1)/2));
		}
		[TestMethod]
		public void TestMedOfMed2()
		{
			int[] array = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue };
			int[] array2 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, int.MaxValue,int.MaxValue,int.MaxValue,int.MaxValue};
			int[] array3 = { 3, 8, 13, 18, 21};
			//Assert.AreEqual(13, PS1.PS1.quickselect(array, array2, (array.Length-1)/2));
			Assert.AreEqual(13, PS1.PS1.Momselect(array, array2, (array.Length-1)/2));
		}
		[TestMethod]
		public void TestBigMedianRandom()
		{
			int[] array = new int[100];
			int[] array2 = new int[100];
			for (int i = 1; i < 101; i+=2)
			{
				array[i - 1] = i;
				array[i] = i+1;
				array2[i -1] =1;
				array2[i] = 1;
			}
			/*
			5
			2 1 4 5 3
			1 1 1 1 1
			 */
			//Assert.AreEqual(array.Length/2, PS1.PS1.quickselect(array, array2, (array.Length-1)/2));
			Assert.AreEqual(array.Length/2, PS1.PS1.Momselect(array, array2, (array.Length-1)/2));
		}
		[TestMethod]
		public void TestBiggliestMedianRandom()
		{
			int[] array = new int[1000000];
			int[] array2 = new int[1000000];
			for (int i = 1; i < 1000001; i+=2)
			{
				array[i - 1] = i;
				array[i] = i-1;
				array2[i -1] =1;
				array2[i] = 1;
			}
			//Assert.AreEqual(array.Length/2, PS1.PS1.quickselect(array, array2, (array.Length-1)/2));
			Assert.AreEqual((array.Length-1)/2, PS1.PS1.Momselect(array, array2, (array.Length-1)/2));
		}
		[TestMethod]
		public void TestBigWeightedMedian()
		{

			int[] array = new int[100];
			int[] array2 = new int[100];
			for (int i = 1; i < 101; i++)
			{
				array[i-1] = i;
				array2[i-1] =1;
			}
			/*
			5
			2 1 4 5 3
			1 1 1 1 1
			 */
			//Assert.AreEqual(70, PS1.PS1.quickselect(array, array2, (array.Length-1)/2));
			Assert.AreEqual(70, PS1.PS1.Momselect(array, array2, (array.Length - 1)/2));
		}

		[TestMethod]
		public void TestBigWeightedMedianRandom()
		{

			int[] array = { 80, 573, 39, 664, 606, 650, 597, 878, 18, 191, 725, 319, 843, 551, 326, 952, 417, 933, 765, 50, 728, 209, 62, 856, 564, 918, 559, 587, 270, 418, 73, 863, 568, 453, 620, 790, 865, 334, 415, 901, 499, 930, 368, 36, 607, 657, 942, 257, 959, 675, 880, 925, 552, 430, 476, 514, 739, 581, 652, 407, 248, 246, 353, 260, 566, 529, 221, 731, 629, 490, 81, 354, 835, 483, 122, 156, 348, 810, 716, 58, 624, 824, 947, 393, 373, 972, 155, 253, 760, 332, 445, 491, 574, 894, 362, 981, 479, 199, 614, 99, 396, 456, 238, 792, 198, 113, 563, 550, 275, 13, 600, 850, 410, 473, 978, 778, 536, 956, 820, 970, 751, 782, 461, 588, 15, 943, 884, 641, 261, 758, 520, 446, 910, 431, 147, 540, 702, 403, 331, 793, 646, 244, 381, 630, 420, 486, 971, 735, 233, 223, 780, 249, 177, 105, 263, 777, 165, 668, 598, 982, 400, 596, 128, 179, 895, 337, 618, 969, 224, 955, 591, 299, 146, 763, 750, 502, 911, 584, 232, 961, 290, 243, 255, 100, 412, 197, 779, 535, 667, 59, 328, 480, 756, 913, 308, 795, 188, 966, 885, 172, 428, 825, 22, 854, 75, 928, 187, 330, 601, 137, 314, 771, 556, 133, 683, 267, 183, 163, 28, 534, 47, 2, 510, 5, 773, 890, 589, 611, 148, 376, 642, 225, 121, 709, 934, 85, 355, 178, 481, 742, 623, 613, 509, 123, 663, 377, 24, 733, 358, 754, 604, 939, 608, 545, 879, 909, 570, 84, 302, 487, 7, 98, 111, 700, 531, 40, 770, 16, 236, 96, 67, 291, 79, 917, 475, 312, 737, 610, 159, 888, 448, 4, 804, 900, 524, 329, 712, 926, 438, 239, 840, 912, 640, 915, 296, 87, 595, 143, 228, 484, 615, 64, 34, 768, 887, 674, 662, 266, 192, 628, 554, 107, 181, 363, 432, 575, 960, 998, 298, 727, 57, 104, 117, 874, 416, 594, 715, 665, 838, 103, 883, 394, 274, 404, 881, 543, 130, 327, 719, 932, 654, 904, 512, 896, 414, 53, 941, 245, 118, 805, 525, 173, 744, 738, 9, 857, 345, 723, 125, 388, 443, 392, 356, 458, 711, 747, 92, 346, 369, 166, 656, 846, 184, 158, 32, 204, 967, 818, 254, 682, 671, 761, 565, 833, 767, 211, 157, 644, 207, 106, 931, 206, 219, 216, 20, 586, 923, 832, 505, 994, 988, 819, 508, 313, 522, 660, 696, 222, 695, 625, 639, 666, 697, 785, 89, 571, 720, 110, 378, 382, 842, 548, 906, 433, 996, 748, 401, 193, 637, 980, 680, 265, 60, 862, 583, 140, 634, 953, 131, 580, 983, 659, 195, 48, 203, 718, 3, 340, 210, 569, 264, 755, 149, 836, 293, 482, 217, 924, 297, 965, 769, 544, 807, 83, 72, 365, 237, 402, 546, 530, 214, 235, 538, 946, 905, 462, 891, 527, 425, 93, 919, 812, 921, 112, 802, 194, 310, 734, 37, 269, 160, 439, 63, 201, 23, 54, 740, 126, 736, 968, 590, 743, 823, 669, 950, 726, 794, 974, 578, 449, 513, 360, 408, 949, 579, 877, 426, 452, 227, 44, 940, 701, 749, 411, 699, 724, 52, 252, 457, 35, 621, 555, 973, 986, 114, 303, 649, 786, 703, 781, 477, 759, 684, 999, 311, 876, 916, 466, 497, 465, 899, 729, 464, 26, 304, 182, 309, 826, 523, 234, 722, 864, 635, 670, 806, 19, 561, 893, 685, 352, 11, 316, 609, 516, 366, 282, 774, 979, 176, 813, 872, 208, 800, 829, 405, 351, 498, 390, 603, 138, 542, 220, 860, 845, 592, 276, 258, 841, 33, 687, 708, 413, 837, 469, 789, 101, 12, 287, 422, 342, 318, 553, 335, 200, 135, 766, 134, 902, 515, 120, 853, 259, 892, 622, 848, 493, 485, 447, 186, 350, 71, 688, 732, 68, 626, 336, 467, 549, 406, 871, 281, 858, 619, 506, 423, 796, 830, 74, 460, 45, 572, 97, 511, 809, 171, 985, 882, 77, 285, 681, 599, 889, 638, 132, 914, 174, 429, 616, 357, 280, 278, 855, 827, 190, 539, 66, 710, 537, 374, 344, 1, 391, 975, 324, 215, 495, 398, 38, 380, 229, 859, 289, 283, 463, 451, 808, 315, 168, 576, 27, 870, 339, 247, 528, 977, 547, 286, 936, 189, 816, 764, 323, 526, 170, 757, 468, 693, 632, 488, 503, 798, 86, 383, 202, 958, 151, 231, 557, 459, 440, 109, 612, 69, 88, 704, 935, 384, 196, 496, 964, 976, 424, 322, 627, 30, 212, 226, 292, 306, 375, 305, 300, 470, 713, 78, 707, 834, 399, 492, 676, 957, 371, 25, 95, 427, 643, 251, 164, 144, 814, 205, 129, 799, 162, 617, 444, 102, 142, 992, 386, 507, 301, 783, 154, 421, 42, 645, 349, 385, 593, 295, 875, 242, 686, 284, 367, 442, 500, 886, 409, 364, 10, 938, 651, 867, 153, 801, 954, 455, 325, 82, 94, 897, 31, 116, 995, 990, 341, 139, 441, 136, 307, 288, 478, 558, 541, 989, 869, 944, 6, 501, 633, 317, 927, 91, 937, 46, 585, 61, 320, 437, 55, 76, 8, 372, 849, 532, 436, 631, 333, 692, 775, 256, 494, 962, 987, 562, 119, 262, 21, 65, 655, 922, 815, 152, 907, 705, 175, 948, 397, 752, 839, 185, 141, 567, 873, 831, 14, 776, 636, 997, 272, 150, 17, 803, 471, 653, 521, 218, 70, 847, 898, 817, 772, 721, 472, 533, 690, 504, 787, 230, 41, 577, 90, 963, 920, 167, 788, 993, 450, 745, 29, 1000, 730, 240, 51, 518, 582, 791, 694, 951, 277, 338, 753, 389, 908, 844, 180, 868, 648, 762, 49, 250, 672, 294, 784, 828, 673, 115, 661, 145, 279, 746, 387, 379, 435, 343, 370, 691, 852, 945, 43, 741, 602, 689, 706, 161, 127, 811, 678, 679, 517, 321, 821, 108, 822, 124, 605, 454, 698, 268, 361, 560, 903, 647, 866, 395, 677, 984, 213, 56, 169, 929, 714, 241, 359, 851, 991, 347, 797, 519, 434, 658, 419, 273, 271, 717, 861, 474, 489 }; ;
			int[] array2 = new int[1000];
			for (int i = 1; i < 1001; i++)
			{
				array2[i-1] =1;
			}
			/*
			5
			2 1 4 5 3
			1 1 1 1 1
			 */
			//Assert.AreEqual(70, PS1.PS1.quickselect(array, array2, (array.Length-1)/2));
			Assert.AreEqual(500, PS1.PS1.Momselect(array, array2, ((array.Length)-1)/2));
		}
	}
}
