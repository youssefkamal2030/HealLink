import 'package:flutter/material.dart';
import 'package:heal_link/core/utils/function/app_colors.dart';
import 'package:heal_link/features/doctor/doctor_auth/presentation/widgets/doctors_sign_up_uploading_image_content.dart';

class DoctorSignUpUploadImageContainer extends StatelessWidget {
  const DoctorSignUpUploadImageContainer({super.key});

  @override
  Widget build(BuildContext context) {
    return Container(
      width: double.infinity,
      padding: EdgeInsets.all(16),
      decoration: BoxDecoration(
        borderRadius: BorderRadius.circular(8),
        border: Border.all(color: AppColors.kLightGreyColor),
      ),
      child: DoctorSignUpUploadImageContent(),
    );
  }
}

