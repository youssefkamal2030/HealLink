
import 'package:flutter/material.dart';
import 'package:heal_link/core/widgets/custom_bottom_hint_of_text_field_part.dart';
import 'package:heal_link/core/widgets/custom_title_of_text_field_section.dart';
import 'package:heal_link/features/doctor/doctor_auth/presentation/widgets/doctor_sign_up_load_image_container.dart';
import 'package:heal_link/generated/l10n.dart';

class DoctorSignUpUploadImageSection extends StatelessWidget {
  const DoctorSignUpUploadImageSection({super.key});

  @override
  Widget build(BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        CustomTitleOfTextFieldSection(
          titleText: S.of(context).upload_syndicate_id,
        ),
        SizedBox(height: 8),
        DoctorSignUpUploadImageContainer(),
        SizedBox(height: 4),

        CustomBottomHintOfTextFeildPart(
          text: "Please upload a clear image of your medical syndicate card",
        ),
      ],
    );
  }
}

