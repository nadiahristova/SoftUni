package Exam6;

import java.util.LinkedHashMap;
import java.util.Map;
import java.util.Scanner;
import java.util.TreeMap;

public class _04_Nuts {
	public static void main(String[] args) {
		Scanner input = new Scanner(System.in);
		int n = Integer.parseInt(input.nextLine());
		TreeMap<String,LinkedHashMap<String,Integer>> out= new TreeMap<>();
		
		for (int i = 0; i < n; i++) {
			String[] line = input.nextLine().trim().split(" ");
			String comp = line[0];
			String nuts = line[1];
			int kg = Integer.parseInt(line[2].substring(0, line[2].length()-2));
			if (!out.containsKey(comp)) {
				out.put(comp, new LinkedHashMap<String,Integer>());
			}
			if (!out.get(comp).containsKey(nuts)) {
				out.get(comp).put(nuts, kg);				
			} else {
				int lastKg = out.get(comp).get(nuts);
				out.get(comp).put(nuts, kg+lastKg);
			}
		}
		
		for(String company:out.keySet()){
			System.out.print(company + ": ");
			boolean boo = true;
			for(Map.Entry<String, Integer> comp: out.get(company).entrySet()){
				if (!boo) {
					System.out.print(", ");
				}
				String nut = comp.getKey();
				int kg = comp.getValue();
				System.out.printf("%s-%dkg",nut,kg);
				boo=false;
			}
			System.out.println();
		}
	}

}
