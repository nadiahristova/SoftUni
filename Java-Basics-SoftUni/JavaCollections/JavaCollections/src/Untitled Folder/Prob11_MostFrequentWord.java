import java.util.HashMap;
import java.util.Scanner;


public class Prob11_MostFrequentWord {
	public static void main(String[] args) {
		Scanner input = new Scanner(System.in); 
		String line = input.nextLine().toLowerCase();
		String[] words = line.split("\\W+");
		
		HashMap<String, Integer> dictionary = new HashMap<>();
		int maxOccur = 0;
		for(String word : words){			
			if (dictionary.containsKey(word)) {
				int currOccur = dictionary.get(word) + 1;
				if (currOccur > maxOccur) {
					maxOccur = currOccur;
				}
				dictionary.put(word, currOccur);
			} else dictionary.put(word, 1);
		}
		
		for (String key : dictionary.keySet()) {
			if (dictionary.get(key) == maxOccur) {
				System.out.println(key + " -> " + dictionary.get(key) + " times");
			}
		}
	}
}
