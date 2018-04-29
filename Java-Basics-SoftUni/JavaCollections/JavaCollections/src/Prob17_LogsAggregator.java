import java.util.Scanner;
import java.util.TreeMap;
import java.util.TreeSet;

public class Prob17_LogsAggregator {
	public static void main(String[] args) {
		Scanner scan = new Scanner(System.in);
		int inputs = Integer.parseInt(scan.nextLine().trim());
		TreeMap<String,TreeSet<String>> userIP = new TreeMap<>();
		TreeMap<String,Integer> userDur = new TreeMap<>();
		
		for (int i = 0; i < inputs; i++) {
			String[] data = scan.nextLine().split(" ");
			String user = data[1];
			String ip = data[0];
			int duration = Integer.parseInt(data[2]);
			
			Integer dur = userDur.get(user);
			if (dur == null) {
				dur = 0;
			}
			userDur.put(user,duration+dur);
			TreeSet<String> ipSet = userIP.get(user);
			if (ipSet == null) {
				ipSet = new TreeSet<>();
			}
			ipSet.add(ip);
			userIP.put(user,ipSet);
		}
		
		for (java.util.Map.Entry<String, Integer> userAndDuration : userDur.entrySet()) {
			String user = userAndDuration.getKey();
			int duration = userAndDuration.getValue();
			TreeSet<String> ipSet = userIP.get(user);
			System.out.println(user + ": " + duration + " " + ipSet);
		}
		
//		for(String user:userDur.keySet()) {
//			System.out.println(user + ": " + userDur.ge+ " " + userIP.get(user) );
//		}
	}

}
