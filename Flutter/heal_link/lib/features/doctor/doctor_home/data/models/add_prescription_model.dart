class AddPrescriptionModel {
  String? medicineName;
  String? frequencyofUse;
  String? usageInstructions;

  AddPrescriptionModel({
    this.medicineName,
    this.frequencyofUse,
    this.usageInstructions,
  });

  AddPrescriptionModel.fromJson(Map<String, dynamic> json) {
    medicineName = json['MedicineName'];
    frequencyofUse = json['FrequencyofUse'];
    usageInstructions = json['Usage Instructions'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = <String, dynamic>{};
    data['MedicineName'] = medicineName;
    data['FrequencyofUse'] = frequencyofUse;
    data['Usage Instructions'] = usageInstructions;
    return data;
  }
}
