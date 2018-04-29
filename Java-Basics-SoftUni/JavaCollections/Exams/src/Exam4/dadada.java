package Exam4;

import java.util.ArrayList;
import java.util.Map;
import java.util.Map.Entry;
import java.util.Scanner;
import java.util.TreeMap;

public class dadada {

	public static void main(String[] args) {
		Scanner scan = new Scanner(System.in);
		int n = Integer.parseInt(scan.nextLine());
		TreeMap<String,TreeMap<String,ArrayList<Double>>> sort = new TreeMap<>();
		
		for (int i = 0; i < n; i++) {
			String[] words = scan.nextLine().split("\\s+");
			String name = words[0] + " " + words[1];
			String subject = words[2];
			double grade = Double.parseDouble(words[3]);
			if (!sort.containsKey(name)) {
				sort.put(name, new TreeMap<String,ArrayList<Double>>());
			}
			if (!sort.get(name).containsKey(subject)) {
				ArrayList<Double> arrList =new ArrayList<Double>();
				arrList.add(grade);
 				sort.get(name).put(subject,arrList);
			} else {
				ArrayList<Double> arrList =sort.get(name).get(subject);
				arrList.add(grade);
				sort.get(name).put(subject,arrList);
			}
		}
		
		for(String name : sort.keySet()){
			System.out.print(name + ": [");
			boolean first = true;
			for (Entry<String, ArrayList<Double>> sub : sort.get(name).entrySet()) {
				String subject = sub.getKey();
				ArrayList<Double> list = sub.getValue();
				double avg = 0;
				for (int i = 0; i < list.size(); i++) {
					avg +=list.get(i);
				}
				avg /= list.size();
				if (!first) {
					System.out.print(", ");
				}
				first = false;
				System.out.printf("%s - %.2f",subject, avg);
			}
			System.out.print("]\n");
		}
	}

}