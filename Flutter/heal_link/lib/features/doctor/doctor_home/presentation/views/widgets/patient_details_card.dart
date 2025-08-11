import 'package:flutter/material.dart';
import 'package:heal_link/features/doctor/doctor_home/presentation/views/widgets/patient_details.dart';
import '../../../../../../core/utils/app_images.dart';
import '../../../../../../core/utils/function/build_home_box_decoration.dart';

class PatientDetailsCard extends StatelessWidget {
  const PatientDetailsCard({super.key});

  @override
  Widget build(BuildContext context) {
    return Container(
      height: 120,
      padding: EdgeInsets.all(16),
      decoration: buildHomeBoxDecoration(isPatient: true),
      child: Row(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          PatientDetails(
            name: 'Ahmed Omar',
            image: AppImages.person,
          ),
          Spacer(),
        ],
      ),
    );
  }
}
