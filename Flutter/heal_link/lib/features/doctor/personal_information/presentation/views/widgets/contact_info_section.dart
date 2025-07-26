import 'package:flutter/cupertino.dart';
import 'package:heal_link/core/utils/app_images.dart';
import 'package:heal_link/core/utils/app_styles.dart';
import 'package:heal_link/features/doctor/personal_information/presentation/views/widgets/info_field.dart';
import 'package:heal_link/generated/l10n.dart';

class ContactInfoSection extends StatelessWidget {
  const ContactInfoSection({super.key});

  @override
  Widget build(BuildContext context) {
    return Column(
      spacing: 8,
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Text(
          S.of(context).contactInfo,
          style: AppTextStyles.popins500style16PrimaryColor,
        ),
        InfoField(
          label: S.of(context).phoneNumber,
          value: '012345678989',
          isEditable: true,
          img: AppImages.edit,
        ),
        InfoField(
          label: S.of(context).email,
          value: 'YousryAhmed123@gmail.com',
          isEditable: true,
          img: AppImages.edit,
        ),
        InfoField(
          label: S.of(context).address,
          value: 'Cairo,Egypt',
          isEditable: true,
          img: AppImages.edit,
        ),
      ],
    );
  }
}
