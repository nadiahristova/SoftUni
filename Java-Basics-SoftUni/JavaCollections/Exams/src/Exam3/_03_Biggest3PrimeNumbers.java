package Exam3;

import java.util.ArrayList;
import java.util.Collections;
import java.util.Scanner;

public class _03_Biggest3PrimeNumbers {
	public static void main(String[] args) {
		Scanner scan = new Scanner(System.in);
		//String input = scan.nextLine().replace(" ", "");
		String[] numsStr = scan.nextLine().split("[()\\s+]+");
		ArrayList<Integer> primeNums = new ArrayList<>();
		ArrayList<Integer> repeatables = new ArrayList<>();
		
		for (int i = 1; i < numsStr.length; i++) {
			int currNum = Integer.parseInt(numsStr[i]);
			if ((currNum > 1 && isPrime(currNum))|| currNum ==2 ) {
				for (int j = 0; j < primeNums.size(); j++) {
					if (currNum == primeNums.get(j)) {
						repeatables.add(currNum);
					}
				}
				primeNums.add(currNum);
			}
		}
		for (int i = 0; i < repeatables.size(); i++) {
			primeNums.remove(repeatables.get(i));
		}
		int size = primeNums.size();
		if ( size < 3) {
			System.out.println("No");
		} else {
			Collections.sort(primeNums);
			int sum = primeNums.get(size-1)+primeNums.get(size-2)+primeNums.get(size-3);
			System.out.println(sum);
		}
		
	}

	private static boolean isPrime(int num) {
		int upTo = (int) Math.pow(num, 0.5)+1;		
		for (int i = 2; i <= upTo; i++) {
			if (num % i == 0) {
				return false;
			}
		}
		return true;
	}
}
