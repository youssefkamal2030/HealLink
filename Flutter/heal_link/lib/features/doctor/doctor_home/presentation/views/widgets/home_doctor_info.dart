import 'package:flutter/material.dart';
import '../../../../../../core/utils/app_images.dart';
import '../../../../../../core/utils/function/build_home_box_decoration.dart';
import '../../../../doctor_auth/presentation/widgets/notification_circle.dart';
import 'doctor_details.dart';

class HomeDoctorInfo extends StatelessWidget {
  const HomeDoctorInfo({super.key});

  @override
  Widget build(BuildContext context) {
    return Container(
      height: 179,
      padding: EdgeInsets.all(16),
      decoration: buildHomeBoxDecoration(),
      child: Column(
        children: [
          SizedBox(height: 40),
          Row(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              DoctorDetails(
                name: 'Dr. Yousry Ahmed',
                type: "Dentist Consultant",
                starNo: 4.9,
                patientNo: 500,
                image: AppImages.person,
              ),
              Spacer(),
              NotificationCircle(),
            ],
          ),
          SizedBox(height: 16),

          // AvailableRow(),
        ],
      ),
    );
  }
}

