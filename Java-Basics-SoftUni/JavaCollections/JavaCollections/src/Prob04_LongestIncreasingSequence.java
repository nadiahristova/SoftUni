import java.util.Scanner;

public class Prob04_LongestIncreasingSequence {
	public static void main(String[] args) {
		Scanner input = new Scanner(System.in);
		String line = input.nextLine();
		String[] strArr = line.trim().split("\\s+");
		int maxSeqLen = 1;
		String longestSeq = strArr + " ";		
		
		for (int i = 0; i < strArr.length; i++) {
			int sequenceLength = 1;
			int counter = 0;
			int startNum = Integer.parseInt(strArr[i]);
			String seq = strArr[i] + " ";
			for (int j = i+1; j < strArr.length; j++) {
				int currNum = Integer.parseInt(strArr[j]);				 
				if (startNum < currNum) {
					seq += strArr[j] + " ";
					sequenceLength++;
					counter++;
					if (maxSeqLen < sequenceLength) {
						maxSeqLen = sequenceLength;
						longestSeq = seq;						
					}
				} else break;
				startNum = currNum;
				i+=counter;
			}
			System.out.println(seq);
		}
		System.out.println("Longest: " + longestSeq);
	}
}
