
import 'package:flutter/material.dart';
import 'package:heal_link/features/doctor/doctor_auth/presentation/widgets/doctors_sign_up_loading_image_content.dart';

class DoctorSignUpSuccessUploadingImageContent extends StatelessWidget {
  const DoctorSignUpSuccessUploadingImageContent({
    super.key,
  });

  @override
  Widget build(BuildContext context) {
    return DoctorSignUpUpLoadingImageContent(uploaded: true , 
    progress: 1,
    );
  }
}
