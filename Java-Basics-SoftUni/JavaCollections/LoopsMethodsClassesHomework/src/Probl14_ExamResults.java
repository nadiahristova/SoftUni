import java.util.HashMap;
import java.util.Map;
import java.util.Scanner;
import java.util.TreeMap;


public class Probl14_ExamResults {
	public static void main(String[] args) {
		Scanner input = new Scanner(System.in);
		
		Map<Integer,TreeMap<String,Double>> sort = new TreeMap<>();
		
		input.nextLine();
		input.nextLine();
		input.nextLine();
		
		String[] elements = input.nextLine().trim().split("\\s*\\|\\s*");
		while (elements.length > 1) {
			int currScore = Integer.parseInt(elements[2]);
			double mark = Double.parseDouble(elements[3]);
			String name = elements[1];
				if (!sort.containsKey(currScore)) {
					sort.put(currScore,new TreeMap<>());
				}	
			sort.get(currScore).put(name, mark);
			elements = input.nextLine().trim().split("\\s*\\|\\s*");
		}		
		
		for(int score:sort.keySet()){
			System.out.print(score + " -> ");
			System.out.print(sort.get(score).keySet());
			double sum= 0D;
			for(double grade:sort.get(score).values()){
				sum+=grade;
			}
			double avg = sum/sort.get(score).values().size();
			System.out.printf("; avg=%.2f\n",avg);
		}
	}
}
