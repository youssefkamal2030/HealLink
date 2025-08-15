import 'package:flutter/material.dart';
import 'package:url_launcher/url_launcher.dart';
import '../../../../../core/widgets/custom_app_bar_pop_widget.dart';
import '../../../../../generated/l10n.dart';

class PrivacyPolicyView extends StatelessWidget {
  const PrivacyPolicyView({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: SingleChildScrollView(
        child: Padding(
          padding: const EdgeInsets.only(
            top: 4.0,
            bottom: 16,
            left: 16,
            right: 16,
          ),
          child: Column(
            children: [
              CustomAppBarPopWidget(
                text: S.of(context).terms_and_privacy_policy,
              ),
              const SizedBox(height: 24),

              // Intro text
              Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Text(
                    S.of(context).privacy_intro_title,
                    style: const TextStyle(
                      color: Color(0xFF2C7BE5),
                      fontSize: 18,
                      fontFamily: 'Poppins',
                      fontWeight: FontWeight.w500,
                    ),
                  ),
                  const SizedBox(height: 3),
                  Text(
                    S.of(context).privacy_intro_subtitle,
                    style: const TextStyle(
                      color: Color(0xFF1C1E22),
                      fontSize: 14,
                      fontFamily: 'Poppins',
                      fontWeight: FontWeight.w500,
                    ),
                  ),
                ],
              ),
              const SizedBox(height: 16),

              // Sections
              BulletSection(
                title: S.of(context).privacy_section1_title,
                bullets: [
                  S.of(context).privacy_section1_bullet1,
                  S.of(context).privacy_section1_bullet2,
                ],
              ),
              BulletSection(
                title: S.of(context).privacy_section2_title,
                bullets: [
                  S.of(context).privacy_section2_bullet1,
                  S.of(context).privacy_section2_bullet2,
                ],
              ),
              BulletSection(
                title: S.of(context).privacy_section3_title,
                bullets: [
                  S.of(context).privacy_section3_bullet1,
                  S.of(context).privacy_section3_bullet2,
                ],
              ),
              BulletSection(
                title: S.of(context).privacy_section4_title,
                bullets: [
                  S.of(context).privacy_section4_bullet1,
                ],
              ),
              BulletSection(
                title: S.of(context).privacy_section5_title,
                bullets: [
                  S.of(context).privacy_section5_bullet1,
                  S.of(context).privacy_section5_bullet2,
                ],
              ),
              BulletSection(
                title: S.of(context).privacy_section6_title,
                bullets: [
                  S.of(context).privacy_section6_bullet1,
                ],
              ),
              BulletSection(
                title: S.of(context).privacy_section7_title,
                bullets: [
                  S.of(context).privacy_section7_bullet1,
                ],
                email: "support@heallink.com",
              ),
            ],
          ),
        ),
      ),
    );
  }
}

/// Reusable widget for bullet sections
class BulletSection extends StatelessWidget {
  final String title;
  final List<String> bullets;
  final String? email;

  const BulletSection({
    super.key,
    required this.title,
    required this.bullets,
    this.email,
  });

  @override
  Widget build(BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Text(
          '$title      ',
          style: const TextStyle(
            color: Color(0xFF2C7BE5),
            fontSize: 12,
            fontFamily: 'Poppins',
            fontWeight: FontWeight.w400,
          ),
        ),
        ...bullets.map(
              (point) => Row(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              const Text(
                '   • ',
                style: TextStyle(
                  color: Colors.black,
                  fontSize: 12,
                  fontFamily: 'Poppins',
                  fontWeight: FontWeight.w400,
                ),
              ),
              Expanded(
                child: Text(
                  point,
                  style: const TextStyle(
                    color: Colors.black,
                    fontSize: 12,
                    fontFamily: 'Poppins',
                    fontWeight: FontWeight.w400,
                  ),
                  maxLines: 5,
                  overflow: TextOverflow.ellipsis,
                ),
              ),
            ],
          ),
        ),
        if (email != null)
          Row(
            children: [
              const SizedBox(width: 20),
              GestureDetector(
                onTap: () async {
                  final Uri uri = Uri(scheme: 'mailto', path: email);
                  if (await canLaunchUrl(uri)) {
                    await launchUrl(uri);
                  }
                },
                child: Text(
                  email!,
                  style: const TextStyle(
                    color: Color(0xFF2C7BE5),
                    decoration: TextDecoration.underline,
                    fontSize: 12,
                    fontFamily: 'Poppins',
                    fontWeight: FontWeight.w400,
                  ),
                ),
              ),
            ],
          ),
        const SizedBox(height: 8),
      ],
    );
  }
}
