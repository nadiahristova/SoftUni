package Exam3;

import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.util.Map;
import java.util.Scanner;
import java.util.TreeMap;

public class _04_ActivityTracker {
	public static void main(String[] args) {
		Scanner scan = new Scanner(System.in);
		int n = Integer.parseInt(scan.nextLine().trim());
		TreeMap<Integer, TreeMap<String, Integer>> out = new TreeMap<>();
		DateTimeFormatter format = DateTimeFormatter.ofPattern("dd/MM/yyyy");
		for (int i = 0; i < n; i++) {
			String[] arrData = scan.nextLine().split(" ");
			LocalDate date = LocalDate.parse(arrData[0], format);
			int month = date.getMonthValue();
			String name = arrData[1];
			int road = Integer.parseInt(arrData[2]);
			if (!out.containsKey(month)) {
				out.put(month, new TreeMap<String, Integer>());
			}
			if (!out.get(month).containsKey(name)) {
				out.get(month).put(name, road);
			} else {
				out.get(month).put(name, out.get(month).get(name) + road);
			}
		}
		
		for (int month : out.keySet()) {
			System.out.print(month+ ": ");
			boolean firstSpot = true;
			for(Map.Entry<String, Integer> list: out.get(month).entrySet()){
				if (!firstSpot) {
					System.out.print(", ");
				}
				String name = list.getKey();
				int roadPassed = list.getValue(); 
				System.out.printf("%s(%d)",name,roadPassed);
				firstSpot =false;
			}
			System.out.println();
		}
	}
}
