import 'package:flutter/material.dart';
import 'package:heal_link/features/doctor/doctor_home/presentation/views/widgets/patient_info_card.dart';
import 'package:heal_link/features/doctor/doctor_home/presentation/views/widgets/patient_request_card.dart';

class TotalPatientTabBarBody extends StatelessWidget {
  const TotalPatientTabBarBody({super.key});

  @override
  Widget build(BuildContext context) {
    return Expanded(
      child: TabBarView(
        children: [
          ListView.builder(
            itemBuilder:
                (context, index) => Padding(
                  padding: const EdgeInsets.only(bottom: 8.0),
                  child: PatientInfoCard(),
                ),
            itemCount: 6,
            padding: EdgeInsets.only(top: 0),
          ),
          ListView.builder(
            itemBuilder:
                (context, index) => Padding(
                  padding: const EdgeInsets.only(bottom: 8.0),
                  child: PatientRequestCard(),
                ),
            itemCount: 8,
            padding: EdgeInsets.only(top: 0),
          ),
        ],
      ),
    );
  }
}
