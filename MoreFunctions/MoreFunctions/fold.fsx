let timeSpent = [30;83;49;12;74;74;86;82;15;43;24;87;3;49;81]

List.fold (fun initial items -> initial + items) 6 timeSpent

let populations = [81276000;67063000;65081000]

List.fold (fun initial populations -> initial + populations) 0 populations

